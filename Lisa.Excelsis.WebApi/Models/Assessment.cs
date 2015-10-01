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
        public IList<Assessor> assessors { get; set; }
        public Student student { get; set; }
        public DateTime Assessed { get; set; }
        public virtual ICollection<Observation> Criteria { get; set; }        
    }
}
