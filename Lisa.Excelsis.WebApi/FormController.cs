using Microsoft.AspNet.Mvc;

namespace Lisa.Excelsis.WebApi
{
    [Route("api/[controller]")]
    public class FormController : Controller
    {
        
        [HttpGet]
        public HttpOkObjectResult Get()
        {
            string[,] forms = new string[3, 2]
            { 
                { "Nederlands", "Lezen" }, 
                { "Rekenen", "Hoofdrekenen" },
                { "AO", "Iteratievergadering" }
            };

            return Ok(forms);
        }
    }
}
