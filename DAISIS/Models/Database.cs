using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DAISIS.Models
{
    public class Database<T> : IDatabase<T> where T : IDatabase<T>, new()
    {
        private const string ConnectionString =
            "Server=dbsys.cs.vsb.cz\\STUDENT;Database=fer0101;User ID=fer0101;Password=savZZc09Tc;";

        private readonly SqlConnection _connection = new SqlConnection(ConnectionString);

        private IEnumerable<T> ExecuteReader(SqlCommand command)
        {
            var result = new List<T>();

            try
            {
                _connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var model = new T();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (reader.GetSchemaTable().Rows.OfType<DataRow>().Any(row => row["ColumnName"].ToString() == property.Name))
                        {
                            var value = reader[property.Name];
                            property.SetValue(model, Convert.IsDBNull(value) ? null : value);
                        }
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

        private void ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                _connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
        }

        public T LoadOne()
        {
            return Load().FirstOrDefault();
        }

        public IEnumerable<T> Load()
        {
            return LoadSql(BuildSelectQueryString());
        }

        public IEnumerable<T> LoadSql(string query)
        {
            return ExecuteReader(GetSqlCommandWithParameters(query));
        }

        public void Save()
        {
            var query = IsInsert() ? BuildInsertQueryString() : BuildUpdateQueryString();
            ExecuteNonQuery(GetSqlCommandWithParameters(query));
        }

        public void RunProcedure(string procedureName)
        {
            ExecuteNonQuery(GetSqlCommandWithProcedure(procedureName));
        }

        private bool IsInsert()
        {
            var model = new T();
            foreach (var property in typeof(T).GetProperties())
            {
                if (PropertyIsKey(property))
                {
                    // todo třeba vyřešit tabulky s více než jedním primarním klíčem
                    if (property.GetValue(this, null) == null)
                    {
                        return true;
                    }

                    property.SetValue(model, property.GetValue(this, null));
                }
            }

            return model.LoadOne() == null;
        }

        private SqlCommand GetSqlCommandWithParameters(string query)
        {
            var command = new SqlCommand(query, _connection);
            AddParamsToQuery(command);
            return command;
        }

        private SqlCommand GetSqlCommandWithProcedure(string procedureName)
        {
            var command = GetSqlCommandWithParameters(procedureName);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        private void AddParamsToQuery(SqlCommand command)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var value = property.GetValue(this, null);
                if (value != null)
                {
                    command.Parameters.AddWithValue("@" + property.Name, value);
                }
            }
        }

        private string BuildSelectQueryString()
        {
            var queryString = $"SELECT * FROM {typeof(T).Name}";
            
            string whereString = null;
            foreach (var property in typeof(T).GetProperties())
            {
                var value = property.GetValue(this, null);
                if (value == null) continue;
                
                whereString = whereString == null ? " WHERE " : whereString + " AND ";
                whereString += FilterEquals(property.Name);
            }

            return queryString + whereString;
        }
        
        private string BuildInsertQueryString()
        {
            var queryString = $"INSERT INTO {typeof(T).Name}";

            string parametersString = null;
            string valuesString = null;
            foreach (var property in typeof(T).GetProperties())
            {
                var value = property.GetValue(this, null);
                if (PropertyIsKey(property) || !PropertyIsEditable(property) || !PropertyIsRequired(property) && value == null) continue;
                parametersString = parametersString == null ? "" : parametersString + ", ";
                parametersString += property.Name;

                valuesString = valuesString == null ? "" : valuesString + ", ";
                valuesString += "@" + property.Name;
            }

            return queryString + " ( " + parametersString + " ) " + " VALUES ( " + valuesString + " ) ";
        }
        
        private string BuildUpdateQueryString()
        {
            var queryString = $"UPDATE {typeof(T).Name}";

            string setString = null;
            string whereString = null;
            foreach (var property in typeof(T).GetProperties())
            {
                if (PropertyIsKey(property))
                {
                    whereString = whereString == null ? " WHERE " : whereString + " AND ";
                    whereString += FilterEquals(property.Name);
                }
                else
                {
                    setString = setString == null ? " SET " : setString + ", ";
                    setString += FilterEquals(property.Name);
                }
            }

            return queryString + setString + whereString;
        }
        
        private string FilterEquals(string propertyName)
        {
            return propertyName + " = @" + propertyName;
        }
        
        public static bool PropertyIsKey(PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(KeyAttribute));
        }
        
        public static bool PropertyIsRequired(PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(RequiredAttribute));
        }
        
        public static bool PropertyIsEditable(PropertyInfo property)
        {
            var attribute = (EditableAttribute) Attribute.GetCustomAttribute(property, typeof(EditableAttribute));
            return attribute == null || attribute.AllowEdit;
        }
    }
}