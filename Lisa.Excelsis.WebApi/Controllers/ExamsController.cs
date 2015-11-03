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
            var result = _db.FetchExams(subject, cohort);
            if (result == null)
            {
                return new HttpNotFoundResult();
            }

            return new HttpOkObjectResult(result);
        }

        private readonly Database _db = new Database();
    }
}