using System.ComponentModel.DataAnnotations;

namespace Lisa.Excelsis.Data
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
