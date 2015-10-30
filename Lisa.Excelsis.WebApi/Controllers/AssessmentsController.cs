using Lisa.Excelsis.Data;
using Lisa.Excelsis.WebApi.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Excelsis.WebApi.Controllers
{
    [Route("[controller]")]
    public class AssessmentsController : Controller
    {
        // GET: assessment
        [HttpGet]
        public IActionResult Get()
        {
            var query = (from a in _db.FetchAssessments()
                         join e in _db.FetchExams()
                         on a.ExamId equals e.Id
                         select new
                         {
                            Id = a.Id,
                            Student = new
                            {
                                Name = a.Student.Name,
                                Number = a.Student.Number
                            },
                            Assessors = a.Assessors.Select(assessors => new
                            {
                                 Username = assessors.Username
                            }),
                            Assessed = new
                            {
                                Date = string.Format("{0}/{1}/{2}", a.Assessed.Day, a.Assessed.Month, a.Assessed.Year),
                                Time = string.Format("{0}:{1}", addZero(a.Assessed.Hour), addZero(a.Assessed.Minute))
                            },
                            Exam = new
                            {
                                Id = e.Id,
                                Name = e.Name,
                                Subject = e.Subject.Name,
                                Cohort = e.Cohort,
                                DocumentationId = e.DocumentationId
                            }
                        });
            if (query == null || query.Count() == 0)
            {
                return new HttpNotFoundObjectResult(new { Error = "No assessments found." });
            }
            return new HttpOkObjectResult(query);
        }

        // GET assessment/{assessmentId}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = (from a in _db.FetchAssessments()
                         join e in _db.FetchExams()
                         on a.ExamId equals e.Id
                         where a.Id == id
                         select new
                         {
                             Id = a.Id,
                             Student = a.Student,
                             Assessors = a.Assessors.Select(assessors => new
                             {
                                 Username = assessors.Username
                             }),
                             Assessed = new
                             {
                                 Date = string.Format("{0}/{1}/{2}", a.Assessed.Day, a.Assessed.Month, a.Assessed.Year),
                                 Time = string.Format("{0}:{1}", addZero(a.Assessed.Hour), addZero(a.Assessed.Minute))
                             },
                             Exam = new
                             {
                                 Id = e.Id,
                                 Name = e.Name,
                                 Subject = e.Subject.Name,
                                 Cohort = e.Cohort,
                                 DocumentationId = e.DocumentationId
                             },
                             Observations = _db.FetchObservations().Where(o => o.AssessmentId == id).Select(o => new
                             {
                                 Id = o.Id,
                                 Criterium = o.Criterium,
                                 Result = o.Result,                                                 
                                 Marks = o.Marks.Split(';')
                             })                            
                         }).FirstOrDefault();

            if (query == null)
            {
                var message = string.Format("The assessment with id {0} is not found.", id);
                return new HttpNotFoundObjectResult(new { Error = message });
            }
            return new HttpOkObjectResult(query);
        }

        [HttpGet("student/{number}")]
        public IActionResult Get(string number)
        {
            var query = (from a in _db.FetchAssessments()
                         join e in _db.FetchExams()
                         on a.ExamId equals e.Id
                         where a.Student.Number.ToLower() == number.ToLower()
                         select new
                         {
                             Id = a.Id,
                             Student = a.Student,
                             Assessors = a.Assessors.Select(assessors => new
                             {
                                Username = assessors.Username
                             }),
                             Assessed = new
                             {
                                 Date = string.Format("{0}/{1}/{2}", a.Assessed.Day, a.Assessed.Month, a.Assessed.Year),
                                 Time = string.Format("{0}:{1}", addZero(a.Assessed.Hour), addZero(a.Assessed.Minute))
                             },
                             Exam = new
                             {
                                 Id = e.Id,
                                 Name = e.Name,
                                 Subject = e.Subject.Name,
                                 Cohort = e.Cohort,
                                 DocumentationId = e.DocumentationId
                             }
                         });
            if (query == null || query.Count() == 0)
            {
                var message = string.Format("No assessments found with student number {0}.", number);
                return new HttpNotFoundObjectResult(new { Error = message});
            }
            return new HttpOkObjectResult(query);
        }

        [HttpPost("/assessments/{subject}/{examName}/{cohort}")]
        public IActionResult Post([FromBody] AssessmentPost assessmentPost, string subject, string examName, string cohort)
        {

            examName = Uri.UnescapeDataString(examName);

            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                .Select(e => e.Exception.Message)
                                .ToList();

                return new BadRequestObjectResult(errorList);
            }
             
            var _exam =  _db.FetchExams().Where(e => e.Subject.Name.ToLower() == subject.ToLower()
                                                  && e.Name.ToLower() == examName.ToLower()
                                                  && e.Cohort.ToLower() == cohort.ToLower()).FirstOrDefault();

            if (_exam != null && assessmentPost != null)
            {
                Assessment assessment = new Assessment();
                List<Assessor> assessors = new List<Assessor>();
                List<Observation> Observations = new List<Observation>();
                               
                var _student = _db.FetchStudents().Where(s => s.Number.ToLower() == assessmentPost.Student.Number.ToLower()).FirstOrDefault();

                if (_student == null)
                {
                    _student = new Student();
                    _student.Name = assessmentPost.Student.Name;
                    _student.Number = assessmentPost.Student.Number;
                    _db.AddStudent(_student);
                }
                
                var _assessors = (from assessorA in _db.FetchAssessors()
                                  join assessorB in assessmentPost.Assessors
                                  on assessorA.Username equals assessorB.UserName
                                  select assessorA);

                foreach (var assessor in assessmentPost.Assessors)
                {
                    if (!_assessors.Any(a => assessor.UserName.Contains(a.Username)))
                    {
                        var a = new Assessor
                        {
                            Username = assessor.UserName
                        };
                        _db.AddAssessor(a);
                    }
                }
                
                assessment.ExamId = _exam.Id;
                assessment.Student = _student;
                assessment.Assessed = assessmentPost.Assessed;
                assessment.Assessors = _assessors.ToList();                
                
                _db.AddAssessment(assessment);

                var _criteria = _db.FetchCriteria().Where(c => c.ExamId == _exam.Id);

                if (_criteria != null)
                {                   
                    foreach (var question in _criteria)
                    {
                         AssessmentPost.ObservationPost  _observation = assessmentPost.Observations?
                                                                        .Where(observation => question.Order == observation.CriteriumOrderId).FirstOrDefault();
                        
                        var o = new Observation();
                        o.AssessmentId = assessment.Id;
                        o.Criterium = question;
                        o.Result = (_observation != null && _observation.Result != null) ? _observation.Result : "";
                        o.Marks = (_observation != null && _observation.Marks != null) ? string.Join(";", _observation.Marks) : "";
                            
                        Observations.Add(o);                        
                    }
                }

                _db.AddObservations(Observations);                

                return new CreatedResult(
                    "http://localhost:5858/assessments/" + assessment.Id, 
                    new
                    {
                        Id = assessment.Id,
                        Assessors = assessment.Assessors.Select(a => new
                        {
                            Id = a.Id,
                            Username = a.Username
                        }),
                        ExamId = assessment.ExamId,
                        Assessed = assessment.Assessed,
                        Student = assessment.Student,
                        Observations = Observations.Where(o => o.AssessmentId == assessment.Id).Select(o => new                                        
                        {
                            Id = o.Id,
                            Criterium = o.Criterium,
                            Result = o.Result,
                            Marks = o.Marks.Split(';')
                        })
                    }
                );
            }
            else
            {
                return new BadRequestResult();
            }            
        }      

        private string addZero(int DateTime)
        {

            return (DateTime < 10) ? "0" + DateTime.ToString() : DateTime.ToString();
        }

        private readonly Database _db = new Database();
    }
}