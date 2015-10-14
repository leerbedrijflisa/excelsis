using Lisa.Excelsis.Data;
using Lisa.Excelsis.WebApi.Models;
using Microsoft.AspNet.Mvc;
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
                            Assessors = (from assessors in a.Assessors
                                        select new
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
            if (query == null)
            {
                return new HttpNotFoundObjectResult(new { message = "No assessments found." });
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

                             Assessors = (from assessors in a.Assessors
                                          select new
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
                             Observations = (from o in _db.FetchObservations()
                                             where o.AssessmentId == id
                                             select new
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
                return new HttpNotFoundObjectResult(new { message = message });
            }
            return new HttpOkObjectResult(query);
        }

        [HttpPost("/assessments/{subject}/{examName}/{cohort}")]
        public IActionResult Post([FromBody] AssessmentPost assessmentPost, string subject, string examName, string cohort)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                .Select(e => e.Exception.Message)
                                .ToList();
                return new ObjectResult(errorList);
            }

            var _exam = (from exams in _db.FetchExams()
                         where exams.Subject.Name.ToLower() == subject.ToLower()
                           && exams.Name.ToLower() == examName.ToLower()
                           && exams.Cohort.ToLower() == cohort.ToLower()
                         select exams).FirstOrDefault();

            if (_exam != null && assessmentPost != null)
            {
                Assessment assessment = new Assessment();
                List<Assessor> assessors = new List<Assessor>();
                List<Observation> Observations = new List<Observation>();
                               
                var _student = (from student in _db.FetchStudents()
                                where student.Number.ToLower() == assessmentPost.Student.Number.ToLower()
                                select student).FirstOrDefault();

                if (_student == null)
                {
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

                var _criteria = (from criterium in _db.FetchCriteria()
                                 where criterium.ExamId == _exam.Id
                                 select criterium);

                if (_criteria != null)
                {                   
                    foreach (var question in _criteria)
                    {
                        if (assessmentPost.Observations != null)
                        {
                            var _observation = (from observation in assessmentPost.Observations
                                               where question.Order == observation.CriteriumOrderId
                                               select observation).FirstOrDefault();

                            var o = new Observation();
                            o.AssessmentId = assessment.Id;
                            o.Criterium = question;
                            o.Result = (_observation.Result != null) ? _observation.Result : "";
                            o.Marks = (_observation.Marks != null) ? string.Join(";", _observation.Marks) : "";
                            
                            Observations.Add(o);
                        }
                    }
                }

                _db.AddObservations(Observations);                

                return new CreatedResult(
                    "http://localhost:5858/assessments/" + assessment.Id, 
                    new
                    {
                        Id = assessment.Id,
                        Assessors = (from a in assessment.Assessors
                                     select new
                                     {
                                         Id = a.Id,
                                         Username = a.Username
                                     }),
                        ExamId = assessment.ExamId,
                        Assessed = assessment.Assessed,
                        Student = assessment.Student,
                        Observations = (from o in Observations
                                        where o.AssessmentId == assessment.Id
                                        select new
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