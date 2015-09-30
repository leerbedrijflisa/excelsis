using System.ComponentModel.DataAnnotations;

namespace Lisa.Excelsis.WebApi.Models
{
    public class Criterium
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public int AssessmentId { get; set; }
        public int Rating { get; set; }
        public bool? Answer { get; set; }
        public bool[] CriteriumBoxes { get; set; }
    }
}
