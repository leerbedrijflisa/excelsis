namespace Lisa.Excelsis.WebApi
{
    public class ExamInfo : IDataObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Cohort { get; set; }
        public string Crebo { get; set; }
    }
}