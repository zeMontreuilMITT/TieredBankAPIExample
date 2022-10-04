using TieredBankAPI.Data;
using TieredBankAPI.Models;

namespace TieredBankAPI.DAL
{
    public class CustomerRepository
    {
        private TieredBankAPIContext _db;

        public CustomerRepository(TieredBankAPIContext db)
        {
            _db = db;
        }

        public ICollection<Customer> GetCustomers()
        {
            return _db.Customer.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _db.Customer.Find(id);
        }

        public void CreateCustomer(Customer customer)
        {
            _db.Customer.Add(customer);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            _db.Customer.Update(customer);
            return customer;
        }

        public void DeleteCustomer(Customer customer)
        {
            _db.Customer.Remove(customer);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
