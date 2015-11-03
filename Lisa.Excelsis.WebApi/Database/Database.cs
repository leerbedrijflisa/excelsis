using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace Lisa.Excelsis.WebApi
{
    interface IDataObject
    {
        int Id { get; set; }
    }

    interface ISubObject
    {
    }

    public class Database
    {
        public IEnumerable<Student> FetchStudents()
        {
            return Select<Student>("Select * from Students");
        }

        public Student FetchStudent(string number)
        {
            return Select<Student>("Select * from Students where Number = @number", new { number = number }).SingleOrDefault();
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
                        if (property.PropertyType.IsConstructedGenericType)
                        {
                            var elementType = property.PropertyType.GetGenericArguments()[0];
                            object o = Activator.CreateInstance(elementType);
                            bool hasValues = false;

                            foreach (var p in elementType.GetProperties())
                            {
                                var value = reader[p.Name];

                                if (!(value is DBNull))
                                {
                                    p.SetValue(o, reader[p.Name]);
                                    hasValues = true;
                                }
                            }

                            if (hasValues)
                            {
                                ((IList)property.GetValue(row)).Add(o);
                            }
                        }
                        else
                        {
                            property.SetValue(row, reader[property.Name]);
                        }
                    }
                }
            }

            return results;
        }
    }
}
