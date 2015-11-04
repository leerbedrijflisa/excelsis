using System;
using System.Collections.Generic;

namespace Lisa.Excelsis.WebApi
{
    public class AssessmentInfo : IDataObject
    {
        public int Id { get; set; }
        public ExamInfo Exam { get; set; }
        public DateTime Assessed { get; set; }
        public Student Student { get; set; }
        public IList<AssessorInfo> Assessors { get; set; }
    }

    public class Assessment : AssessmentInfo
    {
        public IList<Observation> Observations { get; set; }
    }
}