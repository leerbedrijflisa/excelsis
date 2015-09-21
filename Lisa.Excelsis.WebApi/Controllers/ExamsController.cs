using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Lisa.Excelsis.WebApi
{
    [EnableCors("CorsExcelsis")]
    [Route("[controller]")]
    public class ExamsController : Controller
    {
        [HttpGet]
        public object Get()
        {
            var query = (from exams in DummieData.Exams
                         select exams);

            return Json(query);
        }
    }
}
