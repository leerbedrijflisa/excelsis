using Lisa.Excelsis.Data;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Lisa.Excelsis.WebApi.Controllers
{
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var query = _db.FetchStudents();                        

            if (query == null || query.Count() == 0)
            {
                return new HttpNotFoundObjectResult(new { Error = "No students found." });
            }
            return new HttpOkObjectResult(query);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = _db.FetchStudents().Where(s => s.Id == id).FirstOrDefault();
   
            if (query == null)
            {
                var message = string.Format("The student with id {0} is not found.", id);
                return new HttpNotFoundObjectResult(new { Error = message });
            }
            return new HttpOkObjectResult(query);
        }

        private readonly Database _db = new Database();
    }
}
