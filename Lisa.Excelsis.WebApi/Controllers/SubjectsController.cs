using Lisa.Excelsis.Data;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Lisa.Excelsis.WebApi.Controllers
{
    [Route("[controller]")]
    public class SubjectsController : Controller
    {
        // GET: subjects
        [HttpGet]
        public IActionResult Get()
        {
            var query = (from subjects in _db.FetchSubjects()
                         select subjects);

            if (query == null || query.Count() == 0)
            {
                return new HttpNotFoundObjectResult(new { Error = "No subjects found." });
            }
            return new HttpOkObjectResult(query);
        }

        // GET subjects/{subject}
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var query = (from subjects in _db.FetchSubjects()
                         where subjects.Name.ToLower() == name.ToLower()
                         select subjects).FirstOrDefault();

            if (query == null)
            {
                var message = string.Format("The subject with the name {0} is not found.", name);
                return new HttpNotFoundObjectResult(new { Error = message });
            }
            return new HttpOkObjectResult(query);
        }

        private readonly Database _db = new Database();
    }
}
