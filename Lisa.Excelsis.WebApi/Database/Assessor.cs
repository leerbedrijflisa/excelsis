using System.Collections.Generic;

namespace Lisa.Excelsis.WebApi
{
    public class AssessorInfo : IDataObject
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
    public class Assessor : AssessorInfo
    {
        public IList<SubjectInfo> Subjects { get; set; }
    }
}