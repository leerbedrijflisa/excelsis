using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace Lisa.Excelsis.WebApi
{
    [EnableCors("CorsExcelsis")]
    [Route("api/[controller]")]
    public class ExamController : Controller
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
