using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Excelsis.WebApi.Models
{
    public class Assessment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ExamId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public string Examinee { get; set; }
        public List<Criterium> Criteria { get; set; }        
    }
    public class Criterium
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public int AssessmentId { get; set; }
        public int Rating { get; set; }
        public bool? Answer { get; set; }
        public bool[] CriteriumBoxes { get; set; }
    }
}
