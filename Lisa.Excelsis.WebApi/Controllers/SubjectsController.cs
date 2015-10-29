using Lisa.Excelsis.Data;
using Lisa.Excelsis.WebApi.TransferObjects;
using Microsoft.AspNet.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace Lisa.Excelsis.WebApi.Controllers
{
    [Route("[controller]")]
    public class SubjectsController : Controller
    {
        // GET: subjects
        [HttpGet]
        public IActionResult Get([FromQuery] Filter filter )
        {
            IEnumerable query;
            if( filter.Assessor != null)
            {
                query = _db.FetchSubjects().OrderBy(x => x, new CustomCompare(filter.Assessor)).Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name
                });
            }
            else
            {
                query = _db.FetchSubjects().OrderBy(x => x.Name).Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name
                });
            }
            

            if (query == null)
            {
                return new HttpNotFoundObjectResult(new { Error = "No subjects found." });
            }
            return new HttpOkObjectResult(query);
        }       

        // GET subjects/{subject}
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var query = _db.FetchSubjects().Where(s => s.Name.ToLower() == name.ToLower()).Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                Assessors = s.Assessors.Select(a => new
                {
                    Id = a.Id,
                    Username = a.Username
                })
            }).FirstOrDefault();

            if (query == null)
            {
                var message = string.Format("The subject with the name {0} is not found.", name);
                return new HttpNotFoundObjectResult(new { Error = message });
            }
            return new HttpOkObjectResult(query);
        }

        private readonly Database _db = new Database();
    }
}
