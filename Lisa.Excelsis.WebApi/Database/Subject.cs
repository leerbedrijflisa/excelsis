using System.Collections.Generic;

namespace Lisa.Excelsis.WebApi
{
    public class SubjectInfo : IDataObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Subject : SubjectInfo, IDataObject
    {
        public IList<AssessorInfo> Assessors { get; set; }
    }
}