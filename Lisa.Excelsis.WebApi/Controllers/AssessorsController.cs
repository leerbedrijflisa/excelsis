using Lisa.Excelsis.Data;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Lisa.Excelsis.WebApi.Controllers
{
    [Route("[controller]")]
    public class AssessorsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var query = (from assessor in _db.FetchAssessors()
                         select new
                         {
                             id = assessor.Id,
                             username = assessor.Username
                         });

            return new ObjectResult(query);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = (from assessor in _db.FetchAssessors()
                         where assessor.Id == id
                         select new
                         {
                             id = assessor.Id,
                             username = assessor.Username
                         });

            return new ObjectResult(query);
        }

        private readonly Database _db = new Database();
    }
}
