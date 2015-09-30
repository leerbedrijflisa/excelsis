using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Excelsis.WebApi.Models
{
    public class Assessment
    {
        [Key]
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int TeacherId { get; set; }
        public string Examinee { get; set; }
        public DateTime Assessed { get; set; }
        public List<Criterium> Criteria { get; set; }        
    }
}
