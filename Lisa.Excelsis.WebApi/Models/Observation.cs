using System.ComponentModel.DataAnnotations;

namespace Lisa.Excelsis.WebApi.Models
{
    public class Observation
    {
        [Key]
        public int Id { get; set; }
        public Criterium Criterium { get; set; }
        public int AssessmentId { get; set; }
        public bool? Result { get; set; }
        public bool[] Remarks { get; set; }
    }
}
