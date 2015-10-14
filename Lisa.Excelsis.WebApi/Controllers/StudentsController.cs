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
            var query = (from student in _db.FetchStudents()
                         select student);

            if (query == null)
            {
                return new HttpNotFoundObjectResult(new { message = "No students found." });
            }
            return new HttpOkObjectResult(query);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = (from student in _db.FetchStudents()
                         where student.Id == id
                         select student).FirstOrDefault();

            if (query == null)
            {
                var message = string.Format("The student with id {0} is not found.", id);
                return new HttpNotFoundObjectResult(new { message = message });
            }
            return new HttpOkObjectResult(query);
        }

        private readonly Database _db = new Database();
    }
}
