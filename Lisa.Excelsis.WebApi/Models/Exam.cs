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
        public int Subject { get; set; }
        [Required]
        public string Organization { get; set; }
        [Required]
        public string Cohort { get; set; }
        [Required]
        public int DocumentationId { get; set; }

        public ICollection<Question> Questions { get; set; }
    }   
}
