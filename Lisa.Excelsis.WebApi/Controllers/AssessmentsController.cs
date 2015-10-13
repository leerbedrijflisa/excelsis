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
                        from e in _db.FetchExams()
                        where a.ExamId == e.Id
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
            return new ObjectResult(query);
        }

        // GET assessment/{assessmentId}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = (from assessments in _db.FetchAssessments()
                         where assessments.Id == id
                         select new
                         {
                             Id = assessments.Id,
                             Assessors = (from assessors in assessments.Assessors
                                          select new
                                          {
                                              Username = assessors.Username
                                          }),
                             ExamId = assessments.ExamId,
                             Assessed = new
                             {
                                 Date = string.Format("{0}/{1}/{2}", assessments.Assessed.Day, assessments.Assessed.Month, assessments.Assessed.Year),
                                 Time = string.Format("{0}:{1}", addZero(assessments.Assessed.Hour), addZero(assessments.Assessed.Minute))
                             },
                             Student = assessments.Student,
                             Observations = (from observation in _db.FetchObservations()
                                             where observation.AssessmentId == id
                                             select new
                                             {
                                                 Id = observation.Id,
                                                 Criterium = observation.Criterium,
                                                 Result = observation.Result,                                                 
                                                 Marks = observation.Marks.Split(';'),
                                             })
                         }).FirstOrDefault();          

            return new ObjectResult(query);
        }

        [HttpPost("/assessments/{subject}/{examName}/{cohort}")]
        public IActionResult Post([FromBody] AssessmentPost assessmentPost, string subject, string examName, string cohort)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();
                return new ObjectResult(errorList);
            }

            var _exam = (from exams in _db.FetchExams()
                         where exams.Subject.Name.ToLower() == subject.ToLower()
                           && exams.Name.ToLower() == examName.ToLower()
                           && exams.Cohort.ToLower() == cohort.ToLower()
                         select exams).FirstOrDefault();

            var _criteria = (from criterium in _db.FetchCriteria()
                             where criterium.ExamId == _exam.Id
                             select criterium);

            var _assessors = (from assessorA in _db.FetchAssessors()   
                              join assessorB in assessmentPost.Assessors   
                              on assessorA.Username equals assessorB.UserName   
                              select assessorA);

            var _student = (from student in _db.FetchStudents()
                            where student.Name.ToLower() == assessmentPost.Student.Name.ToLower()
                               || student.Number == assessmentPost.Student.Number
                            select student).FirstOrDefault();

            if (_exam != null)
            {
                Assessment assessment = new Assessment();
                List<Assessor> assessors = new List<Assessor>();
                List<Observation> Observations = new List<Observation>();
                Student student = new Student();
                AssessmentPost.ObservationPost observation = new AssessmentPost.ObservationPost();

                if (_student == null)
                {
                    student.Name = assessmentPost.Student.Name;
                    student.Number = assessmentPost.Student.Number;
                    _db.AddStudent(student);
                }
                else
                {
                    student = _student;
                }
   
                foreach(var assessor in assessmentPost.Assessors)
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
                assessment.Student = student;
                assessment.Assessed = assessmentPost.Assessed;
                assessment.Assessors = _assessors.ToList();                
                
                _db.AddAssessment(assessment);

                if (_criteria != null)
                {
                   
                    foreach (var question in _criteria)
                    {
                        if (assessmentPost.Observations != null)
                        {

                            observation = (from o in assessmentPost.Observations
                                           where question.Order == o.CriteriumOrderId
                                           select o).FirstOrDefault();
                        }
                                                
                        if (observation != null && assessmentPost.Observations != null)
                        {
                            Observations.Add(new Observation
                            {
                                AssessmentId = assessment.Id,
                                Criterium = question,
                                Result = observation.Result,
                                Marks = string.Join(";", observation.Marks)

                            });
                        }
                        else
                        {
                            Observations.Add(new Observation
                            {
                                AssessmentId = assessment.Id,
                                Criterium = question,
                                Result = "",
                                Marks = ""
                            });
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
                                        Marks = o.Marks.Split(';'),
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
            if(DateTime < 10)
            {
                return "0" + DateTime.ToString();
            }
            else
            {
                return DateTime.ToString();
            }
        }

        private readonly Database _db = new Database();
    }
}