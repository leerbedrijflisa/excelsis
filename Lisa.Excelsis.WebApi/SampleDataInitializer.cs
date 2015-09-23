using Lisa.Excelsis.WebApi.Models;
using System.Linq;

namespace Lisa.Excelsis.WebApi
{
    public class SampleDataInitializer
    {
        private ExcelsisDb _db;

        public SampleDataInitializer(ExcelsisDb db)
        {
            _db = db;
        }
        public void InitializeDataAsync()
        {           
            CreateSubjects();
            CreateExams();
        }
        private void CreateSubjects()
        {
            if (!_db.Subjects.Any())
            {
                _db.Subjects.Add(new Subject
                {
                    Name = "Nederlands"
                });
                _db.Subjects.Add(new Subject
                {
                    Name = "ApplicatieOntwikkelaar"
                });
                _db.Subjects.Add(new Subject
                {
                    Name = "Engels"
                });
                _db.Subjects.Add(new Subject
                {
                    Name = "Rekenen"
                });
                _db.SaveChanges();
            }
        }
        private void CreateExams()
        {
            if (!_db.Exams.Any())
            {
                Exam exam1 = new Exam
                {
                    Name = "Spreken",
                    Subject = 1,
                    DocumentationId = 1,
                    Cohort = "2015",
                    Organization = "Davinci College Dordrecht"                    
                };

                Exam exam2 = new Exam
                {
                    Name = "Schrijven",
                    Subject = 1,
                    DocumentationId = 1,
                    Cohort = "2014",
                    Organization = "Davinci College Dordrecht"
                };

                Exam exam3 = new Exam
                {
                    Name = "EindProject",
                    Subject = 3,
                    DocumentationId = 1,
                    Cohort = "2015",
                    Organization = "Davinci College Dordrecht"
                };

                _db.Exams.Add(exam1);
                _db.Exams.Add(exam2);
                _db.Exams.Add(exam3);

                _db.SaveChanges();

                _db.Questions.Add(AddQuestionsToExam(exam1.Id));
                _db.Questions.Add(AddQuestionsToExam(exam2.Id));
                _db.Questions.Add(AddQuestionsToExam(exam3.Id));

                _db.SaveChanges();
            }
        }
        private Question AddQuestionsToExam(int id)
        {
            Question question = new Question
            {
                ExamId = id,
                Description = "Some weird question",
                Rating = 1
            };
            return question;
        }
    }
}