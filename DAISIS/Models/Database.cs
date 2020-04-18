﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
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
                
                var properties = typeof(T).GetProperties();

                while (reader.Read())
                {
                    var model = new T();
                    foreach (var property in properties)
                    {
                        if (reader.GetSchemaTable().Rows.OfType<DataRow>().Any(row => row["ColumnName"].ToString() == property.Name))
                        {
                            property.SetValue(model, reader[property.Name]);
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
            return ExecuteReader(GetSqlCommand(query));
        }

        public void Save()
        {
            var query = IsInsert() ? BuildInsertQueryString() : BuildUpdateQueryString();
            
            try
            {
                _connection.Open();
                GetSqlCommand(query).ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
        }

        private bool IsInsert()
        {
            var model = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
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

        private SqlCommand GetSqlCommand(string query)
        {
            var command = new SqlCommand(query, _connection);
            AddParamsToQuery(command);
            return command;
        }

        private void AddParamsToQuery(SqlCommand command)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
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
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
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
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
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
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
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
        
        private bool PropertyIsKey(PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(KeyAttribute));
        }
        
        private bool PropertyIsRequired(PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(RequiredAttribute));
        }
        
        private bool PropertyIsEditable(PropertyInfo property)
        {
            var attribute = (EditableAttribute) Attribute.GetCustomAttribute(property, typeof(EditableAttribute));
            return attribute == null || attribute.AllowEdit;
        }
    }
}