using System.Collections.Generic;

namespace BoringBank.Data
{
    public interface IAccountRepository
    {
        IReadOnlyList<Account> GetAccountsForCustomer(int userId);
        Account GetAccountForCustomer(int customerId, int accountId);
        void Update(Account account);
        void Add(Account account);
    }
}