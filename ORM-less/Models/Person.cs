namespace Models
{
    public class Person : DbObject
    {
        [DbColumn]
        public int ID { get; set; }
        [DbColumn]
        public string FirstName { get; set; } = string.Empty;
        [DbColumn]
        public string LastName { get; set; } = string.Empty;
        [DbColumn]
        public DateTime Created { get; set; }
        [DbColumn]
        public int CreatedByID { get; set; }

        List<Transaction> Transactions { get; set; } = new();
    }
}
