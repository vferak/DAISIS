using System.Collections.Generic;

namespace DAISIS.Models
{
    public interface IDatabase<out T>
    {
        T LoadOne();
        IEnumerable<T> Load();
        IEnumerable<T> LoadSql(string query);
        void Save();
    }
}