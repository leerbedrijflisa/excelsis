using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Excelsis.WebApi.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Organization { get; set; }
        [Required]
        public string Cohort { get; set; }
        [Required]
        public int DocumentationId { get; set; }
        [Required]
        public List<Question> questions { get; set; }
    }
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Rating { get; set; }        
    }
}
