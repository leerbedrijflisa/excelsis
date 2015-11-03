namespace Lisa.Excelsis.WebApi
{
    public class Criterium : IDataObject
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}
