namespace Lisa.Excelsis.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Lisa.Excelsis.Data.ExcelsisDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ExcelsisDb context)
        {
            var dutch = new Subject { Name = "Nederlands" };
            var softwareDevelopment = new Subject { Name = "Applicatieontwikkelaar" };
            var english = new Subject { Name = "Engels" };
            var mathematics = new Subject { Name = "Rekenen" };


            context.Subjects.AddOrUpdate(new Subject[] { dutch, softwareDevelopment, english, mathematics });

            context.Exams.AddOrUpdate(new Exam[] {
                new Exam { Subject = dutch, Name = "Spreken", Cohort = "2015", Organization = "Da Vinci College"},
                new Exam { Subject = dutch, Name = "Schrijven", Cohort = "2015", Organization = "Da Vinci College"},
                new Exam { Subject = english, Name = "Spreken", Cohort = "2015", Organization = "Da Vinci College"},
                new Exam { Subject = softwareDevelopment, Name = "Ontwerpen van een applicatie", Cohort = "2015", Organization = "Da Vinci College"},
                new Exam { Subject = softwareDevelopment, Name = "Realiseren van een applicatie", Cohort = "2015", Organization = "Da Vinci College"},
                new Exam { Subject = softwareDevelopment, Name = "Opleveren van een applicatie", Cohort = "2015", Organization = "Da Vinci College"},
                new Exam { Subject = mathematics, Name = "Verhoudingen", Cohort = "2015", Organization = "Da Vinci College"},
                new Exam { Subject = mathematics, Name = "Meten en meetkunde", Cohort = "2015", Organization = "Da Vinci College"}
            });

            foreach (var exam in context.Exams)
            {
                for (var i = 1; i < 11; i++)
                {
                    context.Criteria.AddOrUpdate(new Criterium
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
