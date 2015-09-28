using Lisa.Excelsis.WebApi.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lisa.Excelsis.WebApi.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ExcelsisDb _db;
        public SubjectsController(ExcelsisDb db)
        {
            _db = db;
        }

        // GET: subjects
        [HttpGet]
        public IActionResult Get()
        {
            var query = (from subjects in _db.Subjects
                         select subjects);

            return new ObjectResult(query);
        }

        // GET subjects/{subject}
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var query = (from subjects in _db.Subjects
                         where subjects.Name.ToLower() == name.ToLower()
                         select subjects);

            return new ObjectResult(query);
        }
    }
}
