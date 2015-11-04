namespace Lisa.Excelsis.WebApi
{
    public class Observation : IDataObject, ISubObject
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public string Result { get; set; }
        public string Marks { get; set; }
        public Criterium Criterium { get; set; }
    }
}