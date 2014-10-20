using System.Collections.Generic;
using System.Linq;
using BoringBank.Data;

namespace BoringBank.Business
{
    public class UserAccountService : IUserAccountService
    {
        public UserAccountService()
        {
            AccountRepository = new AccountRepository("BankingContext");
        }

        #region Dependency Management

        public IAccountRepository AccountRepository { get; set; }

        #endregion

        public IReadOnlyList<Domain.Account> GetAccountsForCustomer(int customerId)
        {
            return AccountRepository.GetAccountsForCustomer(customerId).Select(ToDomain).ToList().AsReadOnly();
        }

        public void RenameAccount(int customerId, int accountId, string newAccountName)
        {
            // TODO : validate arguments ...
            var account = AccountRepository.GetAccountForCustomer(customerId, accountId);

            account.Title = newAccountName;

            AccountRepository.Update(account);
        }

        public void CreateAccountForCustomer(int userId, string accountName)
        {
            // TODO : validate arguments
            var account = new Account()
                          {
                              Balance = 0m,
                              CustomerId = userId,
                              Title = accountName
                          };
            AccountRepository.Add(account);

        }

        public void Transfer(int userId, int fromAccountId, int toAccountId, decimal amountToTransfer)
        {
            // TODO : validate arguments
            var fromAccount = AccountRepository.GetAccountForCustomer(userId, fromAccountId);
            var toAccount = AccountRepository.GetAccountForCustomer(userId, toAccountId);

            // TODO : verify that there is enough money
            fromAccount.Balance -= amountToTransfer;
            toAccount.Balance += amountToTransfer;

            AccountRepository.Update(fromAccount);
            AccountRepository.Update(toAccount);
        }

        private static Domain.Account ToDomain(Account ac)
        {
            return new Domain.Account
                   {
                       Balance = ac.Balance,
                       CustomerId = ac.CustomerId,
                       Id = ac.Id,
                       Name = ac.Title
                   };
        }
    }
}