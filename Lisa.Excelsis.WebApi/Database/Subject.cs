using System.Collections.Generic;

namespace Lisa.Excelsis.WebApi
{
    public class SubjectInfo : IDataObject, ISubObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Subject : SubjectInfo
    {
        public IList<AssessorInfo> Assessors { get; set; }
    }
}