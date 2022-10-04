namespace TieredBankAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    }
}
