using Lisa.Excelsis.WebApi.TransferObjects;
using Microsoft.AspNet.Mvc;

namespace Lisa.Excelsis.WebApi
{
    [Route("[controller]")]
    public class SubjectsController : Controller
    {
        [HttpGet]
        public IActionResult Get([FromQuery] Filter filter)
        {
            string username = (filter.Assessor == null) ? "" : filter.Assessor;
            var results = _db.FetchSubjects(username);
            return new HttpOkObjectResult(results);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var result = _db.FetchSubject(name);
            if (result == null)
            {
                return new HttpNotFoundResult();
            }

            return new HttpOkObjectResult(result);
        }

        private readonly Database _db = new Database();
    }
}