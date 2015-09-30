using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Excelsis.WebApi.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public string Organization { get; set; }
        public string Cohort { get; set; }
        public int DocumentationId { get; set; }
        public List<Question> Questions { get; set; }
    }   
}
