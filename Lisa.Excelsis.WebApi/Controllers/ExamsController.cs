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
            var query = _db.FetchExams();
            return new HttpOkObjectResult(query);
        }

        [HttpGet("{subject}/{name}/{cohort}")]
        public IActionResult Get(string subject, string name, string cohort)
        {
            string examName = name.Replace("-", " ");
            var result = _db.FetchExam(subject, examName, cohort);
            if (result == null)
            {
                return new HttpNotFoundResult();
            }

            return new HttpOkObjectResult(result);
        }

        [HttpGet("{subject}/{cohort}")]
        public IActionResult Get(string subject, string cohort)
        {
            var result = _db.FetchExam(subject, cohort);
            if (result == null)
            {
                return new HttpNotFoundResult();
            }

            return new HttpOkObjectResult(result);
        }

        //[HttpGet("{subject}/{cohort}")]
        //public IActionResult Get(string subject = null, string cohort = null)
        //{
        //    var query = _db.FetchExams().Where(e => e.Subject.Name.ToLower() == subject.ToLower() && e.Cohort == cohort).Select(e => new
        //    {
        //        Id = e.Id,
        //        Name = e.Name,
        //        Subject = e.Subject.Name,
        //        Cohort = e.Cohort,
        //        Crebo = e.Crebo,
        //        Organization = e.Organization
        //    });

        //    if (query == null || query.Count() == 0)
        //    {
        //        var message = string.Format("The exam with subject {0} and cohort {1} is not found.", subject, cohort);
        //        return new HttpNotFoundObjectResult(new { Error = message });
        //    }
        //    else
        //    {
        //        return new HttpOkObjectResult(query);
        //    }
        //}
        private readonly Database _db = new Database();
    }
}