using Lisa.Excelsis.Data;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Excelsis.Data
{
    public class SubjectComparer : IComparer<Subject>
    {
        public SubjectComparer(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public int Compare(Subject x, Subject y)
        {
            if (x.Assessors.Any(a => a.Username.ToLower() == Name.ToLower()))
            {
                if (y.Assessors.Any(a => a.Username.ToLower() == Name.ToLower()))
                {
                    return x.Name.CompareTo(y.Name);
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y.Assessors.Any(a => a.Username.ToLower() == Name.ToLower()))
                {
                    return 1;
                }
                else
                {
                    return x.Name.CompareTo(y.Name);
                }
            }
        }      
    }
}
