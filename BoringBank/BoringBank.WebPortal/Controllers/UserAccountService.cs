using System.Collections.Generic;
using BoringBank.WebPortal.Data;

namespace BoringBank.WebPortal.Controllers
{
    public class UserAccountService
    {
        public IReadOnlyList<Domain.Account> GetAccountsForCustomer(int customerId)
        {
            var accountRepository = new AccountRepository();

            return accountRepository.GetAccountsForCustomer(customerId);
        }

        public void RenameAccount(int customerId, int accountId, string newAccountName)
        {
            // TODO : validate arguments ...
            var accountRepository = new AccountRepository();
            var account = accountRepository.GetAccountForCustomer(customerId, accountId);

            account.Name = newAccountName;

            accountRepository.Update(account);
        }

        public void CreateAccountForCustomer(int userId, string accountName)
        {
            // TODO : validate arguments
            var account = new Domain.Account()
                          {
                              Balance = 0m,
                              CustomerId = userId,
                              Name = accountName
                          };

            var accountRepository = new AccountRepository();
            accountRepository.Add(account);

        }

        public void Transfer(int userId, int fromAccountId, int toAccountId, decimal amountToTransfer)
        {
            // TODO : validate arguments
            var accountRepository = new AccountRepository();
            var fromAccount = accountRepository.GetAccountForCustomer(userId, fromAccountId);
            var toAccount = accountRepository.GetAccountForCustomer(userId, toAccountId);

            // TODO : verify that there is enough money
            fromAccount.Balance -= amountToTransfer;
            toAccount.Balance += amountToTransfer;

            accountRepository.Update(fromAccount);
            accountRepository.Update(toAccount);
        }
    }
}