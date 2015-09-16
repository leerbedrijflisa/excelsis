using System.Collections.Generic;

namespace Lisa.Excelsis.WebApi.Models
{
    public class Assessment
    {
        public int? Id { get; set; }
        public int? ExamId { get; set; }
        public int? TeacherId { get; set; }
        public string Examinee { get; set; }
        public List<Criterium> Criteria { get; set; }        
    }
    public class Criterium
    {
        public int? Id { get; set; }
        public bool MainValue { get; set; }
        public bool[] CriteriumBoxes { get; set; }
    }
}
