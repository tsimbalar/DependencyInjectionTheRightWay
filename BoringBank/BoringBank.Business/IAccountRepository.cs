using System.Collections.Generic;
using BoringBank.Business.Domain;

namespace BoringBank.Business
{
    public interface IAccountRepository
    {
        IReadOnlyList<Account> GetAccountsForCustomer(int userId);
        Account GetAccountForCustomer(int customerId, int accountId);
        void Update(Account account);
        void Add(Account account);
    }
}