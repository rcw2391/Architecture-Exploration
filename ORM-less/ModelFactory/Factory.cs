using Models;
using System.Data.SqlClient;

namespace ModelFactory
{
    public class Factory
    {
        public static IEnumerable<T> ReadResults<T>(SqlDataReader reader) where T: DbObject
        {
            List<T> results = new();
            var properites = typeof(T).GetProperties().Where(p => p.IsDefined(typeof(DbColumn), false));

            while (reader.Read())
            {
                IDbObject row = (IDbObject)Activator.CreateInstance(typeof(T));
                foreach (var property in properites)
                {
                    string? columnOverride = (property.GetCustomAttributes(false).ToList().First(attr => attr is DbColumn) as DbColumn)?.Name;
                    string columnName = columnOverride is null || columnOverride == string.Empty ? property.Name : columnOverride;

                    property.SetValue(row, reader[columnName]);
                    results.Add((T)row);
                }
            }

            return results;
        }
    }
}
