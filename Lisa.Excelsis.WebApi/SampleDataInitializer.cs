using Lisa.Excelsis.WebApi.Models;
using System.Collections.Generic;
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
            var dutch = new Subject { Name = "Nederlands" };
            var softwareDevelopment = new Subject { Name = "Applicatieontwikkelaar" };
            var english = new Subject { Name = "Engels" };
            var mathematics = new Subject { Name = "Rekenen" };

           
            if (!_db.Subjects.Any())
            {
                _db.Subjects.AddRange(new Subject[] { dutch, softwareDevelopment, english, mathematics });
                _db.SaveChanges();

                _db.Exams.AddRange(new Exam[] { 
                    new Exam { Subject = dutch, Name = "Spreken", Cohort = "2015", Organization = "Da Vinci College"},
                    new Exam { Subject = dutch, Name = "Schrijven", Cohort = "2015", Organization = "Da Vinci College"},
                    new Exam { Subject = english, Name = "Spreken", Cohort = "2015", Organization = "Da Vinci College"},
                    new Exam { Subject = softwareDevelopment, Name = "Ontwerpen van een applicatie", Cohort = "2015", Organization = "Da Vinci College"},
                    new Exam { Subject = softwareDevelopment, Name = "Realiseren van een applicatie", Cohort = "2015", Organization = "Da Vinci College"},
                    new Exam { Subject = softwareDevelopment, Name = "Opleveren van een applicatie", Cohort = "2015", Organization = "Da Vinci College"},
                    new Exam { Subject = mathematics, Name = "Verhoudingen", Cohort = "2015", Organization = "Da Vinci College"},
                    new Exam { Subject = mathematics, Name = "Meten en meetkunde", Cohort = "2015", Organization = "Da Vinci College"}
                });
                _db.SaveChanges();
                
                foreach (var exam in _db.Exams)
                {                   
                    for(var i = 0; i < 10; i++)
                    {
                        _db.Questions.Add(new Question
                        {
                            Description = "Hier hoord vraag " + i + " te staan.",
                            Rating = 0,
                            ExamId = exam.Id
                        });
                    }  
                }
                _db.SaveChanges();
            }
        }
    }
}