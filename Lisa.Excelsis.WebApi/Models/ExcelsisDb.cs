using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace Lisa.Excelsis.WebApi.Models
{
    public class ExcelsisDb : DbContext
    {   
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Assessor> Assessors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Result> Results { get; set; }               
    }
}
