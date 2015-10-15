namespace Lisa.Excelsis.Data.Migrations
{
    using System.Collections;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Lisa.Excelsis.Data.ExcelsisDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ExcelsisDb context)
        {

            var joost = new Assessor { Username = "joostronkesagerbeek" };

            context.Assessors.AddOrUpdate(a => a.Username, new Assessor[] { joost });

            var dutch = new Subject { Name = "Nederlands", Assessors = new Assessor[] { joost } };
            var softwareDevelopment = new Subject { Name = "Applicatieontwikkelaar"};
            var english = new Subject { Name = "Engels" };
            var mathematics = new Subject { Name = "Rekenen", Assessors = new Assessor[] { joost } };


            context.Subjects.AddOrUpdate(s => s.Name, new Subject[] { dutch, softwareDevelopment, english, mathematics });

            context.Exams.AddOrUpdate(e => e.Crebo, new Exam[] {
                new Exam { Subject = dutch, Name = "Spreken", Cohort = "2015", Crebo = "9001", Organization = "Da Vinci College"},
                new Exam { Subject = dutch, Name = "Schrijven", Cohort = "2015", Crebo = "9002", Organization = "Da Vinci College"},
                new Exam { Subject = english, Name = "Spreken", Cohort = "2015", Crebo = "9003", Organization = "Da Vinci College"},
                new Exam { Subject = softwareDevelopment, Name = "Ontwerpen van een applicatie", Cohort = "2015", Crebo = "9004", Organization = "Da Vinci College"},
                new Exam { Subject = softwareDevelopment, Name = "Realiseren van een applicatie", Cohort = "2015", Crebo = "9005", Organization = "Da Vinci College"},
                new Exam { Subject = softwareDevelopment, Name = "Opleveren van een applicatie", Cohort = "2015", Crebo = "9006", Organization = "Da Vinci College"},
                new Exam { Subject = mathematics, Name = "Verhoudingen", Cohort = "2015", Crebo = "9007", Organization = "Da Vinci College"},
                new Exam { Subject = mathematics, Name = "Meten en meetkunde", Cohort = "2015", Crebo = "9008", Organization = "Da Vinci College"}
            });

            foreach (var exam in context.Exams)
            {
                for (var i = 1; i < 11; i++)
                {
                    context.Criteria.AddOrUpdate(c => c.ExamId, new Criterium
                    {
                        Description = "De kandidaat doet wat van hem verwacht wordt.",
                        value = "V",
                        Order = i,
                        ExamId = exam.Id
                    });
                }
            }
        } 
    }
}
