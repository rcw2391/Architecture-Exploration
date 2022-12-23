namespace Models
{
    [DbTable("Person")] 
    public class Person : DbObject
    {
        [DbColumn]
        [DbPk]
        public int ID { get; set; }
        [DbColumn]
        public string FirstName { get; set; } = string.Empty;
        [DbColumn]
        public string LastName { get; set; } = string.Empty;
        [DbColumn]
        public DateTime Created { get; set; }
        [DbColumn]
        public int CreatedByID { get; set; }

        [DbFk("PersonID", typeof(Transaction))]
        public List<Transaction> Transactions { get; set; } = new();
    }
}
