using Lisa.Excelsis.Data;
using Lisa.Excelsis.WebApi.TransferObjects;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Lisa.Excelsis.WebApi.Controllers
{
    [Route("[controller]")]
    public class SubjectsController : Controller
    {
        [HttpGet]
        public IActionResult Get([FromQuery] Filter filter)
        {
            var subjects = _db.FetchSubjects(filter.Assessor);
            var result = SubjectMapper.ToTransferObjects(subjects);
            return new HttpOkObjectResult(result);
        }

        // GET subjects/{subject}
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var subject = _db.FetchSubjects().Where(s => s.Name.ToLower() == name.ToLower());
            var result = SubjectMapper.ToTransferObject(subject);
            return new HttpOkObjectResult(result);
        }

        private readonly Database _db = new Database();
    }
}
