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

            return new ObjectResult(query);
        }

        // GET subjects/{subject}
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var query = (from subjects in _db.FetchSubjects()
                         where subjects.Name.ToLower() == name.ToLower()
                         select subjects);

            return new ObjectResult(query);
        }

        private readonly Database _db = new Database();
    }
}
