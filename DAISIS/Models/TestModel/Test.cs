using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DAISIS.Models.TestModel
{
    public class Test
    {
        private object[] _models =
        {
            new Designers(), new Events(), new Games(), new Publishers(), new Thread_comments(), new Threads(),
            new User_events(), new User_game_rankings(), new User_thread_rankings(), new Users()
        };

        public string Run()
        {
            var result = "";

            // result += Create();
            // result += Read();
            // result += Update();
            // result += Delete();
            
            return result;
        }

        private string Create()
        {
            var result = "";
            foreach (var model in _models)
            {
                foreach (var property in model.GetType().GetProperties())
                {
                    if (Database<Games>.PropertyIsKey(property) ||
                        !Database<Games>.PropertyIsEditable(property)) continue;
                    
                    if (property.PropertyType == typeof(int?))
                    {
                        property.SetValue(model, 1);
                    }
                    else if (property.PropertyType == typeof(bool?))
                    {
                        property.SetValue(model, true);
                    }
                    else if (property.PropertyType == typeof(DateTime?))
                    {
                        property.SetValue(model, DateTime.Now);
                    }
                    else if (property.Name == "email")
                    {
                        property.SetValue(model, "test@test.cz");
                    }
                    else if (property.Name == "password")
                    {
                        property.SetValue(model, "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08");
                    }
                    else
                    {
                        property.SetValue(model, property.Name + " - testovací text");
                    }
                    
                }
                CallMethod("Save", model);
            }

            return result;
        }
        
        private string Read()
        {
            var result = "";
            foreach (var model in _models)
            {
                var loadedModel = CallMethod("LoadOne", model);
                foreach (var property in loadedModel.GetType().GetProperties())
                {
                    result += property.Name + " - " + property.GetValue(loadedModel, null) + "\n";
                }

                result += "\n\n";
            }

            return result;
        }

        private object CallMethod(string methodName, object model)
        {
            return model.GetType().GetMethod(methodName)?.Invoke(model, new object[]{});
        }
    }
}