namespace Models
{
    public class Transaction : DbObject
    {
        [DbColumn]
        public int ID { get; set; }
        [DbColumn]
        public DateTime Created { get; set; }
        [DbColumn]
        public decimal Amount { get; set; }
    }
}
