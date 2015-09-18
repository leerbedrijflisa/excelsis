using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;

namespace Lisa.Excelsis.WebApi
{
    [EnableCors("CorsExcelsis")]
    [Route("api/[controller]")]
    public class ExamController : Controller
    {
        
        [HttpGet]
        public HttpOkObjectResult Get()
        {
            string[,] forms = new string[5, 2]
            { 
                { "Nederlands", "Lezen" }, 
                { "Rekenen", "Hoofdrekenen" },
                { "AO", "Iteratievergadering" },
                { "Nederlands", "Spreken" },
                { "Rekenen", "Getallen" }
            };

            return Ok(forms);
        }
    }
}
