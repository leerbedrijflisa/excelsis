using Lisa.Excelsis.Data;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Lisa.Excelsis.WebApi
{
    [EnableCors("CorsExcelsis")]
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
                             Organization = exam.Organization
                         });

            return new ObjectResult(query);
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

            return new ObjectResult(query);
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
                             Organization = exams.Organization
                         }); ;        

            return new ObjectResult(query);
        }
        private readonly Database _db = new Database();
    }
}
