using Lisa.Excelsis.WebApi.Models;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Lisa.Excelsis.WebApi
{
    [EnableCors("CorsExcelsis")]
    [Route("[controller]")]
    public class ExamsController : Controller
    {
        private readonly ExcelsisDb _db;

        public ExamsController(ExcelsisDb db)
        {
            _db = db;
        }

        [HttpGet]
        public object Get()
        {
            var query = (from exams in _db.Assessments
                         select exams);

            return Json(query);
        }
    }
}
