using Lisa.Excelsis.Data;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Lisa.Excelsis.WebApi.Controllers
{
    [Route("[controller]")]
    public class ExamsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var query = _db.FetchExams().Select(e => new
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Subject = e.Subject.Name,
                            Cohort = e.Cohort,
                            Crebo = e.Crebo,
                            Organization = e.Organization
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
            var query = _db.FetchExams().Where(e => e.Id == id).Select(e => new
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Subject = e.Subject.Name,
                            Cohort = e.Cohort,
                            Crebo = e.Crebo,
                            Organization = e.Organization,
                            Criteria = _db.FetchCriteria().Where(c => c.ExamId == id).Select(c => new
                                       {
                                           Id = c.Id,
                                           Description = c.Description,
                                           Rating = c.value
                                       })
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
            var query = _db.FetchExams().Where(e => e.Subject.Name.ToLower() == subject.ToLower() && e.Cohort == cohort).Select(e => new
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Subject = e.Subject.Name,
                            Cohort = e.Cohort,
                            Crebo = e.Crebo,
                            Organization = e.Organization
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
