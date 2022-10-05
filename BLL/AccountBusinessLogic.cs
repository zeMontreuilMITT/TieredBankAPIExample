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

        public void Deposit(int id, decimal amount)
        {
            Account account = _repo.GetAccountByID(id);

            if (account == null)
            {
                throw new KeyNotFoundException();
            } else
            {
                if (amount <= 0)
                {
                    throw new ArgumentException("Deposit amount must be greater than zero");
                }
                account.Balance += amount;
                _repo.Save(); 
            }
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

/*[1:41 PM] Zacharie Montreuil
Create an endpoint for Depositing money into an account, when the Account ID is providedAdd a "is active" property (bool) to an account. If it's not active, it can't be deposited to or withdrawn from.Add an endpoint for switching an account between active or inactive

*/