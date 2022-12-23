namespace Models
{
    public class DbTable : Attribute
    {
        public DbTable(string tableName)
        {
            TableName = tableName;
        }

        public string TableName { get; set; }
    }
}
