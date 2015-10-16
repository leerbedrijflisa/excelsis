using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Excelsis.Data
{
    public partial class Database
    {
        public IEnumerable<Subject> FetchSubjects(string assessor = null)
        {
            if (assessor == null)
            {
                return _db.Subjects.OrderBy(subject => subject.Name);
            }
            else
            {
                // Note that the comparer can't be translated to SQL, so we have to
                // fetch the subject from the database (that is what ToList() does) and
                // then let C# take care of the sorting.
                var comparer = new SubjectComparer(assessor);
                return _db.Subjects
                    .ToList()
                    .OrderBy(subject => subject, comparer);
            }
        }

        public Subject AddSubject(Subject subject)
        {
            var query = _db.Subjects.Add(subject);
            _db.SaveChanges();

            return query;
        }

        public IEnumerable<Subject> AddSubjects(IEnumerable<Subject> subjects)
        {
            var query = _db.Subjects.AddRange(subjects);
            _db.SaveChanges();

            return query;
        }

        public bool AnySubject()
        {
            return _db.Subjects.Any();
        }
    }
}