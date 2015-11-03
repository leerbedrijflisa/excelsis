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
            var results = _db.FetchSubjects();
            return new HttpOkObjectResult(results);
        }

        //// GET subjects/{subject}
        //[HttpGet("{name}")]
        //public IActionResult Get(string name)
        //{
        //    var result = _db.Select<Subject>(
        //          "Select * from Subjects " +
        //          "where Name = '" + name + "'");

        //    return new HttpOkObjectResult(result);
        //}

        private readonly Database _db = new Database();
    }
}
