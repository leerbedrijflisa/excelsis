﻿using Microsoft.AspNet.Mvc;

namespace Lisa.Excelsis.WebApi
{
    [Route("[controller]")]
    public class AssessorsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = _db.FetchAssessors();
            return new HttpOkObjectResult(result);
        }

        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var result = _db.FetchAssessor(username);
            if (result == null)
            {
                return new HttpNotFoundResult();
            }

            return new HttpOkObjectResult(result);
        }

        private readonly Database _db = new Database();
    }
}