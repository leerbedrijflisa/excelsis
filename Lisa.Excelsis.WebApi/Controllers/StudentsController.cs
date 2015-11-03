using Microsoft.AspNet.Mvc;

namespace Lisa.Excelsis.WebApi
{
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = _db.FetchStudents();
            return new HttpOkObjectResult(result);
        }

        [HttpGet("{number}")]
        public IActionResult Get(string number)
        {
            var result = _db.FetchStudent(number);
            if (result == null)
            {
                return new HttpNotFoundResult();
            }
            
            return new HttpOkObjectResult(result);
        }

        private readonly Database _db = new Database();
    }
}