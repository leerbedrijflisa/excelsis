﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//{
//    "student": {
//        "name": "Arthur van Strien",
//        "studentNumber": "99887766"
//    },
//    "teacher": [{
//        "userName": "joostronkesagerbeek"
//    }],
//    "assessed": "2015-09-29T14:07:34Z"
//}

namespace Lisa.Excelsis.WebApi.Models
{
    public class AssessmentPost
    {
        public StudentPost Student { get; set; }
        public IEnumerable<TeacherPost> Teacher { get; set; }
        public DateTime Assessed { get; set; }
    }

    public class StudentPost
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }

    public class TeacherPost
    {
        public string UserName { get; set; }
    }
}
