using Models;
using System.Collections;
using System.Data.SqlClient;
using System.Net.Security;
using System.Reflection;
using DbColumn = Models.DbColumn;

namespace ModelFactory
{
    public class Factory
    {
        // Needs to be refactored to be more readable and useable. However it works, it can pull a single object and it can pull a child
        // object. To be added in the future: recursively populate child objects if the data exists.
        // Refactor ideas: Implement classes to handle work so this is easier to follow, manage, and write tests for.
        public static IEnumerable<T> ReadResults<T>(SqlDataReader reader) where T: DbObject
        {
            List<T> results = new();
            var properites = typeof(T).GetProperties().Where(p => p.IsDefined(typeof(DbColumn), false));
            var columnSchema = reader.GetColumnSchema();
            var childProperties = typeof(T).GetProperties().Where(p => p.IsDefined(typeof(DbFk), false));
            Dictionary<PropertyInfo?, Type> childPropertiesToPopulate = new();
            var primaryKey = typeof(T).GetProperties().First(p => p.IsDefined(typeof(DbPk), false));
            Dictionary<(string TableName, string ColumnName), int> ordinals = new();

            foreach (var column in columnSchema.Where(c => c?.ColumnOrdinal is not null && c?.BaseColumnName is not null && c?.BaseTableName is not null))
            {
                ordinals.Add((column.BaseTableName, column.ColumnName), column.ColumnOrdinal.Value);
            }

            foreach (var childProperty in childProperties)
            {
                var dbFkAttr = childProperty.GetCustomAttributes(false).ToList().First(attr => attr is DbFk) as DbFk;
                if (dbFkAttr?.ColumnName is null) continue;
                Type childPropertyType = dbFkAttr?.Type is null ? dbFkAttr.GetType() : dbFkAttr.Type;
                string? childPropertyTableName = (childPropertyType.GetCustomAttributes(false).ToList().FirstOrDefault(attr => attr is DbTable) as DbTable)?.TableName;
                if (childPropertyTableName is not null && columnSchema.Any(c => c.BaseTableName == childPropertyTableName))
                {
                    childPropertiesToPopulate.Add(childProperty, childPropertyType);
                }
            }

            while (reader.Read())
            {
               var existingObject = results.FirstOrDefault(r => typeof(T).GetProperty(primaryKey.Name).GetValue(r).Equals(reader[primaryKey.Name]));
                bool existingObjectIsNull = existingObject is null;
                T row = existingObjectIsNull ? (T)Activator.CreateInstance(typeof(T)) : existingObject;
                SetValues(properites, row, reader, ordinals);
                

                foreach (var childProperty in childPropertiesToPopulate.Keys)
                {
                    Type childType = childPropertiesToPopulate[childProperty];
                    IDbObject child = (IDbObject)Activator.CreateInstance(childPropertiesToPopulate[childProperty]);
                    SetValues(childType.GetProperties(), child, reader, ordinals);

                    var type = childProperty.GetValue(row).GetType();

                    if (type.GetInterface(nameof(IEnumerable)) is not null && type != typeof(IEnumerable<char>) && type != typeof(string))
                    {
                        var children = childProperty.GetValue(row);
                        object[] obj = new object[1] { child };
                        children.GetType().GetMethod("Add").Invoke(children, obj);
                    } else
                    {
                        childProperty.SetValue(row, child);
                    }
                }
                if (existingObjectIsNull) results.Add(row);
            }

            return results;
        }

        private static void SetValues(IEnumerable<PropertyInfo?> properties, object o, SqlDataReader reader, Dictionary<(string TableName, string ColumnName), int> ordinals)
        {
            string? tableName = (o.GetType().GetCustomAttribute(typeof(DbTable)) as DbTable).TableName;
            foreach (var property in properties)
            {
                string? columnOverride = (property.GetCustomAttributes(false).ToList().FirstOrDefault(attr => attr is DbColumn) as DbColumn)?.Name;
                string columnName = columnOverride is null || columnOverride == string.Empty ? property.Name : columnOverride;

                if (ordinals.ContainsKey((tableName, columnName))) property.SetValue(o, reader.GetValue(ordinals[(tableName, columnName)]));
            }
        }
    }
}
