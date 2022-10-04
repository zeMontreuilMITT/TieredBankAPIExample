using TieredBankAPI.DAL;
using TieredBankAPI.Models;

namespace TieredBankAPI.BLL
{
    public class AccountBusinessLogic
    {
        private AccountRepository _repo;
        public AccountBusinessLogic(AccountRepository repo)
        {
            _repo = repo;
        }

        public decimal GetBalance(int id)
        {
            Account account = _repo.GetAccountByID(id);

            if(account != null)
            {
                return account.Balance;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public decimal GetSumOfBalances(int customerId)
        {
            List<Account> accounts = _repo.GetAccountByCustomerId(customerId);

            return accounts.Sum(a => a.Balance);
        }

        public decimal Withdraw(int accountId, decimal amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentException();
            } else
            {
                Account account = _repo.GetAccountByID(accountId);

                if(account == null)
                {
                    throw new KeyNotFoundException();
                }
                else
                {
                    account.Balance -= amount;
                    return account.Balance;
                }
            }
        }
    }
}
