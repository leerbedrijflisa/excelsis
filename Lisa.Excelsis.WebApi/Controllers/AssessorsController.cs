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

            if (query == null || query.Count() == 0)
            {
                return new HttpNotFoundObjectResult(new { Error = "No assessors found." });
            }
            return new HttpOkObjectResult(query);
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
                         }).FirstOrDefault();

            if (query == null)
            {
                var message = string.Format("The assessor with id {0} is not found.", id);
                return new HttpNotFoundObjectResult(new { Error = message });
            }
            return new HttpOkObjectResult(query);
        }

        private readonly Database _db = new Database();
    }
}
