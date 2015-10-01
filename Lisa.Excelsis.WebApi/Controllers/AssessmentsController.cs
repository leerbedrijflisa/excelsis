using Lisa.Excelsis.WebApi.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Excelsis.WebApi.Controllers
{
    [Route("[controller]")]
    public class AssessmentsController : Controller
    {
        private readonly ExcelsisDb _db;
        public AssessmentsController(ExcelsisDb db)
        {
            _db = db;
        }
        // GET: assessment
        [HttpGet]
        public IActionResult Get()
        {
            var query = (from assessments in _db.Assessments
                         select new
                         {
                             Id = assessments.Id,
                             Student = new 
                             {
                                 Name = assessments.student.Name,
                                 Number = assessments.student.Number
                             },
                             Assessor = (from assessors in assessments.assessors                                         
                                        select new
                                        {
                                            Username = assessors.Username
                                        }),
                             Assessed = assessments.Assessed,
                             ExamId = assessments.ExamId
                         });

            return new ObjectResult(query);
        }

        // GET assessment/{assessmentId}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = (from assessments in _db.Assessments
                         where assessments.Id == id
                         select new
                         {
                             Id = assessments.Id,
                             Assessor = assessments.assessors,
                             ExamId = assessments.ExamId,
                             Assessed = assessments.Assessed,
                             Student = assessments.student,
                             Criteria = (from observation in _db.Observations
                                         where observation.AssessmentId == id
                                         select observation)
                         });          

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

            var _exam = (from exams in _db.Exams
                         where exams.Subject.Name.ToLower() == subject.ToLower()
                           && exams.Name.ToLower() == examName.ToLower()
                           && exams.Cohort.ToLower() == cohort.ToLower()
                         select exams).FirstOrDefault();

            var _criteria = (from criterium in _db.Criteria
                             where criterium.ExamId == _exam.Id
                             select criterium);

            var _assessors = (from assessor in _db.Assessors
                              where assessmentPost.Assessors.All(a => a.UserName.Contains(assessor.Username))
                              select assessor);

            var _student = (from student in _db.Students
                            where student.Name.ToLower() == assessmentPost.Student.Name.ToLower()
                               || student.Number == assessmentPost.Student.Number
                            select student).FirstOrDefault();

            if (_exam != null)
            {
                Assessment assessment = new Assessment();
                IList<Assessor> assessors = new List<Assessor>();
                IList<Observation> Criteria = new List<Observation>();
                Student student = new Student();

                if (_student == null)
                {
                    student.Name = assessmentPost.Student.Name;
                    student.Number = assessmentPost.Student.Number;
                    _db.Students.Add(student);
                }
                else
                {
                    student = _student;
                }

                if(_assessors == null || _assessors.Count() != assessmentPost.Assessors.Count())
                {
                    foreach(var assessor in assessmentPost.Assessors)
                    {
                        assessors.Add(new Assessor
                        {
                            Username = assessor.UserName
                        });
                    }
                    _db.Assessors.AddRange(assessors);
                }
                else
                {
                    assessors = _assessors.ToList();
                }
                
                _db.SaveChanges();               

                assessment.ExamId = _exam.Id;
                assessment.student = student;
                assessment.Assessed = assessmentPost.Assessed;
                assessment.assessors = assessors;                
                
                _db.Assessments.Add(assessment);
                _db.SaveChanges();

                if (_criteria == null)
                {
                    foreach (var question in _criteria)
                    {
                        Criteria.Add(new Observation
                        {
                            AssessmentId = assessment.Id,
                            Criterium = question,
                            Result = null,
                            Remarks = new bool[]
                            {
                                false,false,false,false
                            }
                        });
                    }

                    _db.Observations.AddRange(Criteria);
                }

                _db.SaveChanges();

                return new ObjectResult(assessment);
            }
            else
            {
                return new BadRequestResult();
            }            
        }

        // PATCH api/assessment/5
        [HttpPatch("{assesmentId}")]
        public void Patch(int assesmentId, [FromBody]string value)
        {

        }

        // PATCH api/assessment/5/3
       /* [HttpPatch("{assesmentId}/{criteriumId}")]
        public IActionResult Patch([FromBody]Observation value, int assesmentId, int criteriumId)
        {
            var query = (from criteria in _db.Criterium
                         where criteria.Id == criteriumId && criteria.AssessmentId == assesmentId
                         select criteria).SingleOrDefault();

            if (ModelState.IsValid)
            {
                if (value.Answer != null)
                {
                    query.Answer = value.Answer;
                }

                if (value.CriteriumBoxes != null)
                {
                    query.CriteriumBoxes = value.CriteriumBoxes;
                }
                _db.SaveChanges();

                return new ObjectResult(query);
            }
            else
            {
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
                return new ObjectResult(errorList);
            }               
        }*/
    }
}