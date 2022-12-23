namespace Models
{
    public class DbFk : Attribute
    {
        public DbFk(string columnName)
        {
            ColumnName = columnName;
        }

        public DbFk(string columnName, Type type)
        {
            ColumnName = columnName;
            Type = type;
        }
        
        public string ColumnName { get; set; }
        public Type Type { get; set; }
    }
}
