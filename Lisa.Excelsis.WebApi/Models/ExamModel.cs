using System.Collections.Generic;

namespace Lisa.Excelsis.WebApi.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Organization { get; set; }
        public string Cohort { get; set; }
        public int DocumentationId { get; set; }
        public List<Question> questions { get; set; }
    }
    public class Question
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }        
    }
}
