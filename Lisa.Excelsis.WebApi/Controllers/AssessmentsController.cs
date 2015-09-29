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
        public object Get()
        {
            var query = (from assessments in _db.Assessments
                         select new
                         {
                             Id = assessments.Id,
                             Examinee = assessments.Examinee,
                             TeacherId = assessments.TeacherId,
                             ExamId = assessments.ExamId
                         });

            return Json(query);
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
                                  TeacherId = assessments.TeacherId,
                                  ExamId = assessments.ExamId,
                                  Examinee = assessments.Examinee,
                                  Criteria = from criterium in _db.Criterium
                                             where criterium.AssessmentId == id
                                             select criterium
                              });          

            return new ObjectResult(query);
        }

        [HttpPost("/assessments/{subject}/{exam}/{cohort}")]
        public IActionResult PostPost([FromBody] AssessmentPost assessment, string subject, string exam, string cohort)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            return new ObjectResult(assessment);
        }

        //POST assessment
        [HttpPost]
        public IActionResult Post([FromBody] Assessment value)
        {
            if (ModelState.IsValid)
            {
                Assessment assessment = new Assessment();

                var exam = (from exams in _db.Exams
                             where exams.Id == value.ExamId
                             select exams).FirstOrDefault();

                assessment.ExamId = value.ExamId;
                assessment.TeacherId = value.TeacherId;
                assessment.Examinee = value.Examinee;
                assessment.Criteria = new List<Criterium>();

                _db.Assessments.Add(assessment);
                _db.SaveChanges();
                    
                var questions = (from criterium in _db.Questions
                                where criterium.ExamId == value.ExamId
                                select criterium);

                foreach (var question in questions)
                {
                    assessment.Criteria.Add(new Criterium
                    {
                        Question = question.Description,
                        Rating = question.Rating,
                        AssessmentId = assessment.Id,
                        Answer = null,
                        CriteriumBoxes = new bool[]
                        {
                            false,false,false,false
                        }
                    });
                }
               
                _db.SaveChanges();

                return new ObjectResult(assessment);
            }
            else
            {
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
                return new ObjectResult(errorList);
            }
        }

        // PATCH api/assessment/5
        [HttpPatch("{assesmentId}")]
        public void Patch(int assesmentId, [FromBody]string value)
        {

        }

        // PATCH api/assessment/5/3
        [HttpPatch("{assesmentId}/{criteriumId}")]
        public IActionResult Patch([FromBody]Criterium value, int assesmentId, int criteriumId)
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
        }
    }
}