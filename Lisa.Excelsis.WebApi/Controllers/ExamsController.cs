using Lisa.Excelsis.WebApi.Models;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Excelsis.WebApi
{
    [EnableCors("CorsExcelsis")]
    [Route("[controller]")]
    public class ExamsController : Controller
    {
        private readonly ExcelsisDb _db;

        public ExamsController(ExcelsisDb db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var query = (from exam in _db.Exams
                         select new
                         {
                             Id = exam.Id,
                             Name = exam.Name,
                             Subject = exam.Subject.Name,
                             Cohort = exam.Cohort,
                             Organization = exam.Organization
                         });

            return new ObjectResult(query);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = (from exam in _db.Exams
                         where exam.Id == id
                         select new
                         {
                             Id = exam.Id,
                             Name = exam.Name,
                             Subject = exam.Subject.Name,
                             Cohort = exam.Cohort,
                             Organization = exam.Organization,
                             Questions = from question in _db.Questions
                                         where question.ExamId == id
                                         select new
                                         {
                                             Id = question.Id,
                                             Description = question.Description,
                                             Rating = question.Rating
                                         }
                         });
            return new ObjectResult(query);
        }

        [HttpGet("{subject}/cohort/{cohort}")]
        public IActionResult Get(string subject = null, string cohort = null)
        {
            var query = (from exams in _db.Exams
                         where exams.Subject.Name.ToLower() == subject.ToLower() &&
                         exams.Cohort == cohort
                         select exams);        

            return new ObjectResult(query);
        }
    }
}
