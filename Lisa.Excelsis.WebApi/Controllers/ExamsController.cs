using Lisa.Excelsis.Data;
using Microsoft.AspNet.Mvc;
using System;
using System.Linq;

namespace Lisa.Excelsis.WebApi.Controllers
{
    [Route("[controller]")]
    public class ExamsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var query = (from exam in _db.FetchExams()
                         select new
                         {
                             Id = exam.Id,
                             Name = exam.Name,
                             Subject = exam.Subject.Name,
                             Cohort = exam.Cohort,
                             Crebo = exam.Crebo,
                             Organization = exam.Organization
                         });

            if(query == null || query.Count() == 0)
            {
                return new HttpNotFoundObjectResult(new { Error = "No exams found." });
            }
            return new HttpOkObjectResult(query);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = (from exam in _db.FetchExams()
                         where exam.Id == id
                         select new
                         {
                             Id = exam.Id,
                             Name = exam.Name,
                             Subject = exam.Subject.Name,
                             Cohort = exam.Cohort,
                             Crebo = exam.Crebo,
                             Organization = exam.Organization,
                             Criteria = from criterium in _db.FetchCriteria()
                                         where criterium.ExamId == id
                                         select new
                                         {
                                             Id = criterium.Id,
                                             Description = criterium.Description,
                                             Rating = criterium.value
                                         }
                         }).FirstOrDefault();

            if (query == null)
            {
                var message = string.Format("The exam with id {0} is not found.", id);
                return new HttpNotFoundObjectResult(new { Error = message });
            }
            return new HttpOkObjectResult(query);
        }

        [HttpGet("{subject}/{cohort}")]
        public IActionResult Get(string subject = null, string cohort = null)
        {
            var query = (from exams in _db.FetchExams()
                         where exams.Subject.Name.ToLower() == subject.ToLower() &&
                         exams.Cohort == cohort
                         select new
                         {
                             Id = exams.Id,
                             Name = exams.Name,
                             Subject = exams.Subject.Name,
                             Cohort = exams.Cohort,
                             Crebo = exams.Crebo,
                             Organization = exams.Organization
                         }).FirstOrDefault();

            if (query == null)
            {
                var message = string.Format("The exam with subject {0} and cohort {1} is not found.", subject, cohort);
                return new HttpNotFoundObjectResult( new { Error = message });
            }
            return new HttpOkObjectResult(query);
        }
        private readonly Database _db = new Database();
    }
}
