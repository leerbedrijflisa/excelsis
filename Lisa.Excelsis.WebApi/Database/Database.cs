using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Reflection;

namespace Lisa.Excelsis.WebApi
{  
    public class Database
    {
        public IEnumerable<Student> FetchStudents()
        {
            var query = "Select * from Students";
            return Select<Student>(query);
        }

        public Student FetchStudent(string number)
        {
            var query = @"Select * 
                          from Students
                          where Students.Number = @number";
            var parameters = new { number = number };
            return Select<Student>(query, parameters).SingleOrDefault();
        }

        public IEnumerable<SubjectInfo> FetchSubjects(string username)
        {
            var query = @"IF EXISTS (SELECT * FROM Assessors WHERE Username = @username)
                            BEGIN
                                Select Subjects.*
                                from Subjects
                                left join SubjectAssessors on Subject_Id = Subjects.Id
                                left join Assessors on Assessors.Id = Assessor_Id
                                order by case Username
                                    when @username then Username
                                end
                                desc
                            END
                        ELSE
                            BEGIN
                                Select *
                                from Subjects
                                order by Name asc
                            END";
            var parameters = new { username = username };
            return Select<SubjectInfo>(query, parameters);
        }

        public Subject FetchSubject(string name)
        {
            var query = @"Select *, Subjects.Id as SubjectId,
                                    Subjects.Name as SubjectName, 
                                    Assessors.Id as AssessorsId
                          from Subjects                           
                          left join SubjectAssessors on Subject_Id = Subjects.Id
                          left join Assessors on Assessors.Id = Assessor_Id
                          where Subjects.Name = @name";
            var parameters = new { name = name };
            return Select<Subject>(query, parameters).SingleOrDefault();
        }

        public IEnumerable<ExamInfo> FetchExams(string subject = null, string cohort = null)
        {
            var query = @"Select *, Subjects.Id as SubjectId,
                                    Subjects.Name as SubjectName
                          from Exams                           
                          left join Subjects on Subjects.Id = Exams.Subject_Id";

            if( subject != null && cohort != null)
            {
                query = query + " where Subjects.Name = @subject and Exams.Cohort = @cohort";
                var parameters = new { subject = subject, cohort = cohort };
                return Select<ExamInfo>(query, parameters);
            }

            return Select<ExamInfo>(query);
        }

        public Exam FetchExam(string subject, string name, string cohort)
        {
            var query = @"Select *, Subjects.Id as SubjectId,
                                    Subjects.Name as SubjectName,
                                    Criteriums.Id as CriteriumsId
                          from Exams                           
                          left join Subjects on Subjects.Id = Exams.Subject_Id
                          left join Criteriums on Criteriums.ExamId = Exams.Id
                          where Subjects.Name = @subject and Exams.Name = @name and Exams.Cohort = @cohort";
            var parameters = new { subject = subject, name = name, cohort = cohort };
            return Select<Exam>(query, parameters).SingleOrDefault();
        }

        public IEnumerable<AssessorInfo> FetchAssessors()
        {
            var query = "Select * from Assessors";
            return Select<AssessorInfo>(query);
        }

        public Assessor FetchAssessor(string username)
        {
            var query = @"Select *, Assessors.Id as AssessorsId,
                                    Subjects.Id as SubjectsId,
                                    Subjects.Name as SubjectsName                                    
                          from Assessors                           
                          left join SubjectAssessors on Assessor_Id = Assessors.Id
                          left join Subjects on Subjects.Id = Subject_Id
                          where Assessors.Username = @username";
            var parameters = new { username = username };
            return Select<Assessor>(query, parameters).SingleOrDefault();
        }

        public IEnumerable<AssessmentInfo> FetchAssessments()
        {
            var query = @"Select *, Assessors.Id as AssessorsId,
                                    Exams.Id as ExamId, Exams.Name as ExamName,
                                    Subjects.Id as SubjectId, Subjects.Name as SubjectName, 
                                    Students.Id as StudentId, Students.Name as StudentName    
                          from Assessments          
                          left join Students on Students.Id = Assessments.Student_Id
                          left join Exams on Exams.Id = Assessments.ExamId
                          left join Subjects on Subjects.Id = Exams.Subject_Id
                          left join AssessorAssessments on Assessment_Id = Assessments.Id
                          left join Assessors on Assessors.Id = Assessor_Id"; 
            return Select<AssessmentInfo>(query);
        }

        public Assessment FetchAssessment(int id)
        {
            var query = @"Select *, Assessors.Id as AssessorsId,
                                    Observations.Id as ObservationsId,
                                    Criteriums.Id as CriteriumId,
                                    Exams.Id as ExamId, Exams.Name as ExamName,
                                    Subjects.Id as SubjectId, Subjects.Name as SubjectName, 
                                    Students.Id as StudentId, Students.Name as StudentName                                
                          from Assessments          
                          left join Students on Students.Id = Assessments.Student_Id
                          left join Exams on Exams.Id = Assessments.ExamId
                          left join Subjects on Subjects.Id = Exams.Subject_Id
                          left join AssessorAssessments on Assessment_Id = Assessments.Id
                          left join Assessors on Assessors.Id = Assessor_Id
                          left join Observations on Observations.AssessmentId = Assessments.Id
                          left join Criteriums on Criteriums.Id = Observations.Criterium_Id
                          where Assessments.Id = @id";
            var parameters = new { id = id };
            return Select<Assessment>(query, parameters).SingleOrDefault();
        }

        private IEnumerable<T> Select<T>(string query, object parameters = null) where T : IDataObject, new()
        {
            var results = new List<T>();
            using (var connection = new SqlConnection(@"Data Source=(localdb)\v11.0;Initial Catalog=ExcelsisDb;Integrated Security=True"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    foreach (var parameter in parameters.GetType().GetProperties())
                    {
                        command.Parameters.Add(new SqlParameter(parameter.Name, parameter.GetValue(parameters)));
                    }
                }

                var reader = command.ExecuteReader();
                T row = new T();

                while (reader.Read())
                {
                    if (((int)reader["Id"]) != row.Id)
                    {
                        row = new T();
                        results.Add(row);
                    }

                    foreach (var property in typeof(T).GetProperties())
                    {
                        Something(reader, row, property);
                    }
                } 
            }

            return results;
        }
        private bool Something(SqlDataReader reader, dynamic row, PropertyInfo property, string propertyName = null)
        {
            if (property.PropertyType.IsConstructedGenericType)
            {
                var elementType = property.PropertyType.GetGenericArguments()[0];
                IList list;

                if (property.GetValue(row) == null)
                {
                    Type listType = typeof(List<>).MakeGenericType(elementType);
                    list = (IList)Activator.CreateInstance(listType);
                    property.SetValue(row, list);
                }
                else
                {
                    list = (IList)property.GetValue(row);
                }

                object o = Activator.CreateInstance(elementType);
                bool hasValues = false;

                foreach (var p in elementType.GetProperties())
                {
                    if (!Something(reader, o, p, property.Name))
                    {
                        var value = reader[p.Name];

                        if (!(value is DBNull))
                        {
                            string name = (p.Name == "Id" || p.Name == "Name") ? property.Name + p.Name : p.Name;
                            if (!(reader[name] is DBNull))
                            {
                                p.SetValue(o, reader[name]);
                                hasValues = true;
                            }
                        }
                    }
                }

                if (hasValues)
                {
                    list.Add(o);
                }
                return true;
            }
            else if (Implements(property, typeof(ISubObject)))
            {
                var elementType = property.PropertyType;
                object o = Activator.CreateInstance(elementType);
                bool hasValues = false;

                foreach (var p in elementType.GetProperties())
                {
                    if(!Something(reader, o, p))
                    {
                        var value = reader[p.Name];

                        if (!(value is DBNull))
                        {
                            string name = (p.Name == "Id" || p.Name == "Name") ? property.Name + p.Name : p.Name;
                            p.SetValue(o, reader[name]);
                            hasValues = true;
                        }
                    }          
                }

                if (hasValues)
                {
                    property.SetValue(row, o);
                }
                return true;
            }
            else
            {
                property.SetValue(row, reader[property.Name]);
            }
            return false;
        }    

        private bool Implements(PropertyInfo property, Type interfaceType)
        {
            foreach (var @interface in property.PropertyType.GetInterfaces())
            {
                if (@interface == interfaceType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}