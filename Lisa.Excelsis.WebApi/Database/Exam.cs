using System.Collections.Generic;

namespace Lisa.Excelsis.WebApi
{
    public class ExamInfo : IDataObject, ISubObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Cohort { get; set; }
        public string Crebo { get; set; }
        public SubjectInfo Subject { get; set; }
    }
    public class Exam : ExamInfo
    {
        public IList<Criterium> Criteriums { get; set; }
    }
}