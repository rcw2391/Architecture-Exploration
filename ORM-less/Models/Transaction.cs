namespace Models
{
    [DbTable("Transactions")]
    public class Transaction : DbObject
    {
        [DbColumn]
        [DbPk]
        public int ID { get; set; }
        [DbColumn]
        public DateTime Created { get; set; }
        [DbColumn]
        public decimal Amount { get; set; }
        [DbColumn]
        public int PersonID { get; set; }
    }
}
