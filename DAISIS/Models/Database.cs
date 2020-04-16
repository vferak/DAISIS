﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DAISIS.Models
{
    public class Database<T> where T : new()
    {
        private const string ConnectionString =
            "Server=dbsys.cs.vsb.cz\\STUDENT;Database=fer0101;User ID=fer0101;Password=savZZc09Tc;";

        private readonly SqlConnection _connection = new SqlConnection(ConnectionString);

        public IEnumerable<T> Load(string[] parameters = null)
        {
            var result = new List<T>();

            var command = GetSelectCommand(parameters);

            try
            {
                _connection.Open();

                var reader = command.ExecuteReader();

                var properties = GetProperties(parameters);

                while (reader.Read())
                {
                    var model = new T();
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

        private PropertyInfo[] GetProperties(string[] parameters = null)
        {
            var properties = typeof(T).GetProperties();

            if (parameters != null)
            {
                properties = Array.FindAll(properties, property => parameters.Contains(property.Name));
            }

            return properties;
        }

        public void Save(T model)
        {
            var isInsert = ModelIsInDatabase(model);

            try
            {
                _connection.Open();
            }
            finally
            {
                _connection.Close();
            }
        }

        private bool ModelIsInDatabase(T model)
        {
            var keyProperty = typeof(T).GetProperties()
                .Where(property => property.IsDefined(typeof(KeyAttribute), false));
            var result = Load();
            return true;
        }

        private SqlCommand GetSelectCommand(string[] parameters = null)
        {
            var queryString = BuildQueryString(parameters);

            var query = new SqlCommand(queryString, _connection);

            AddParamsToQuery(query, parameters);

            return query;
        }

        private string BuildQueryString(string[] parameters = null)
        {
            var queryString = "SELECT";

            if (parameters != null)
            {
                var i = 0;
                var length = parameters.Length;
                foreach (var parameter in parameters)
                {
                    queryString += $" {parameter}";

                    if (++i != length)
                    {
                        queryString += ", ";
                    }
                }
            }
            else
            {
                queryString += " *";
            }

            queryString += $" FROM {typeof(T).Name}";

            return queryString;
        }

        private void AddParamsToQuery(SqlCommand query, string[] parameters = null)
        {
        }

        private object ConvertToCorrectDataType(PropertyInfo property, string value)
        {
            if (property.PropertyType == typeof(int))
            {
                return Int32.Parse(value);
            }

            if (property.PropertyType == typeof(bool))
            {
                return Boolean.Parse(value);
            }

            if (property.PropertyType == typeof(DateTime))
            {
                return DateTime.Parse(value);
            }

            return value;
        }
    }
}