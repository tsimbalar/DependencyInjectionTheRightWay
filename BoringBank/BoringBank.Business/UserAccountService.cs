using System;
using System.Collections.Generic;
using BoringBank.Business.Domain;

namespace BoringBank.Business
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public UserAccountService(IAccountRepository accountRepository)
        {
            if (accountRepository == null) throw new ArgumentNullException("accountRepository");
            _accountRepository = accountRepository;
        }

        #region Dependency Management

        public IAccountRepository AccountRepository { get { return _accountRepository; } }

        #endregion

        public IReadOnlyList<Domain.Account> GetAccountsForCustomer(int customerId)
        {
            return AccountRepository.GetAccountsForCustomer(customerId);
        }

        public void RenameAccount(int customerId, int accountId, string newAccountName)
        {
            // TODO : validate arguments ...
            var account = AccountRepository.GetAccountForCustomer(customerId, accountId);

            account.Name = newAccountName;

            AccountRepository.Update(account);
        }

        public void CreateAccountForCustomer(int userId, string accountName)
        {
            // TODO : validate arguments
            var account = new Account()
                          {
                              Balance = 0m,
                              CustomerId = userId,
                              Name = accountName
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
    }
}