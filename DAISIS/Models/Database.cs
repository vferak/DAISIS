using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

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
                var tableName = typeof(T).Name;
                var query = new SqlCommand($"SELECT * FROM {tableName}", _connection);

                _connection.Open();

                var reader = query.ExecuteReader();

                while (reader.Read())
                {
                    var model = new T();
                    var properties = typeof(T).GetProperties();
                    foreach (var property in properties)
                    {
                        var value = ConvertToCorrectDataType(property, reader[property.Name].ToString());

                        property.SetValue(model, value);
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

        private object ConvertToCorrectDataType(PropertyInfo property, string value)
        {
            if (property.PropertyType == typeof(int))
            {
                return Int32.Parse(value);
            }

            return value;
        }
    }
}