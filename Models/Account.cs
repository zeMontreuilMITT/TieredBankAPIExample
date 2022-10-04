using System.ComponentModel.DataAnnotations;

namespace TieredBankAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
