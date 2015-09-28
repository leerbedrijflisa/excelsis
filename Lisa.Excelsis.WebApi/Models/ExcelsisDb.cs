using Microsoft.Data.Entity;

namespace Lisa.Excelsis.WebApi.Models
{
    public class ExcelsisDb : DbContext
    {   
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Assessor> Assessors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Criterium> Criterium { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Result> Results { get; set; }               
    }
}
