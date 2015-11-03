using System;
using System.Collections.Generic;

namespace Lisa.Excelsis.WebApi
{
    public class AssessmentInfo : IDataObject
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public DateTime Assessed { get; set; }
        public Student Student { get; set; }
    }

    public class Assessment
    {
        public IList<AssessorInfo> Assessors { get; set; }
    }
}