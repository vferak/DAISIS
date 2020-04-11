using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAISIS.Models
{
    public class Model<T>
    {
        public static IEnumerable<T> Load()
        {
            SqlConnection connection = null;
            
            try  
            {
                connection = new SqlConnection(
                    "Server=dbsys.cs.vsb.cz\\STUDENT;" +
                    "Database=fer0101;" +
                    "User ID=fer0101;" +
                    "Password=savZZc09Tc;"
                    );
            
                var query = new SqlCommand($"SELECT * FROM publishers", connection);
                
                connection.Open();
                
                var reader = query.ExecuteReader();
                while (reader.Read())
                {  
                    Console.WriteLine(reader["email"]);
                }  
            }  
            catch (Exception e)  
            {
                Console.WriteLine("OOPs, something went wrong." + e);  
            }
            finally  
            {  
                connection?.Close();  
            }
            
            return new List<T>();
        }
    }
}