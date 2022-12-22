namespace Models
{
    public class DbColumn : Attribute {
        public string Name { get; set; } = string.Empty;
        public DbColumn() { }
        public DbColumn(string name) => Name = name;
    }
}
