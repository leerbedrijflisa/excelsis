using Lisa.Excelsis.WebApi.Models;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
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
            var query = (from exams in _db.Exams
                         select exams);

            return Json(query);
<<<<<<< HEAD
        }

        [HttpGet("{subjectId}/cohort/{cohort}")]
        public object Get(int? subjectId = null, string cohort = null)
        {
            string method = "GET";
            string request = "/subject/" + subjectId + "/cohort/" + cohort;

            List<string> errors = new List<string>();

            var query = (from exams in _db.Exams
                         where exams.Subject == subjectId &&
                         exams.Cohort == cohort
                         select exams.Criteria)();

            if (query == null)
            {
                errors.Add("No exams where found");
                return HttpError(request, method, 404, errors);
            }

            return Json(query);
        }

        // This creates a HTTP error code with json data
        public object HttpError(string request, string method, int HttpStatusCode, List<string> message)
        {
            Response.StatusCode = HttpStatusCode;
            var error = new Error();
            error.HttpResponseCode = HttpStatusCode;
            error.Request = request;
            error.Method = method;
            error.message = message;

            return Json(error);
        }
=======
        }        
>>>>>>> develop
    }
}
