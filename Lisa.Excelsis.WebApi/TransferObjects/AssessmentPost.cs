using System;
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
        public List<AssessorPost> Assessors { get; set; }
        public DateTime Assessed { get; set; }
        public List<ObservationPost> Observations { get; set; }

        public class StudentPost
        {
            public string Name { get; set; }
            public string Number { get; set; }
        }

        public class AssessorPost
        {
            public string UserName { get; set; }
        }
        public class ObservationPost
        {
            public int CriteriumOrderId { get; set; } 
            public string Result { get; set; }
            public List<string> Marks { get; set; }
        }
    }   
}
