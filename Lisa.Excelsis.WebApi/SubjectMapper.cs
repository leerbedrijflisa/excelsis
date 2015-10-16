using Lisa.Excelsis.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Excelsis.WebApi
{
    public static class SubjectMapper
    {
        public static IEnumerable<object> ToTransferObject(IEnumerable<Subject> subjects)
        {
            return subjects.Select(subject => new
            {
                Id = subject.Id,
                Name = subject.Name
            });
        }
    }
}