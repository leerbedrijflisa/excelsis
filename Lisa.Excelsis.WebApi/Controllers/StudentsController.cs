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

            return new ObjectResult(query);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = (from student in _db.FetchStudents()
                         where student.Id == id
                         select student);
            return new ObjectResult(query);
        }

        private readonly Database _db = new Database();
    }
}
