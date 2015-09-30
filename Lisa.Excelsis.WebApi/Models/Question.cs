using System.ComponentModel.DataAnnotations;

namespace Lisa.Excelsis.WebApi.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int ExamId { get; set; }
    }
}
