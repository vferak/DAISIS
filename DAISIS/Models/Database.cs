using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAISIS.Models
{
    public class Database<T> where T : new()
    {
        private const string ConnectionString =
            "Server=dbsys.cs.vsb.cz\\STUDENT;Database=fer0101;User ID=fer0101;Password=savZZc09Tc;";

        private readonly SqlConnection _connection = new SqlConnection(ConnectionString);

        public IEnumerable<T> Load()
        {
            var result = new List<T>();
            
            try
            {
                var tableName = typeof(T).Name.Replace("Model", "");
                var query = new SqlCommand($"SELECT * FROM {tableName}", _connection);

                _connection.Open();

                var reader = query.ExecuteReader();

                while (reader.Read())
                {
                    var model = new T();
                    var properties = typeof(T).GetProperties();
                    foreach (var property in properties)
                    {
                        typeof(T).GetProperty(property.Name)?.SetValue(model, reader[property.Name]);
                    }
                    
                    result.Add(model);
                }
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }
    }
}