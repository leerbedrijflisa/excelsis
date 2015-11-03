namespace Lisa.Excelsis.WebApi
{
    public class Student : IDataObject, ISubObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }
}