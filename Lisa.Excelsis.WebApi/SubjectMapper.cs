using Lisa.Excelsis.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Excelsis.WebApi
{
    public static class SubjectMapper
    {
        public static IEnumerable<object> ToTransferObjects(IEnumerable<Subject> subjects)
        {
            return subjects.Select(subject => new
            {
                Id = subject.Id,
                Name = subject.Name
            });
        }
        public static dynamic ToTransferObject(IEnumerable<Subject> subjects)
        {
            return subjects.Select(subject => new
            {
                Id = subject.Id,
                Name = subject.Name,
                Assessors = subject.Assessors.Select(assessor => new
                {
                    Id = assessor.Id,
                    Username = assessor.Username
                })
            }).FirstOrDefault();
        }
    }
}