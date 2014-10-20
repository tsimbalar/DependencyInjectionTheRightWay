using System;
using System.Collections.Generic;
using System.Linq;
using BoringBank.Business;
using BoringBank.Business.Domain;

namespace BoringBank.WebPortal
{
    public class CachedAccountRepository : IAccountRepository
    {
        private readonly ICache _cache;
        private readonly IAccountRepository _decorated;

        public CachedAccountRepository(ICache cache, IAccountRepository decorated)
        {
            if (cache == null) throw new ArgumentNullException("cache");
            if (decorated == null) throw new ArgumentNullException("decorated");
            _cache = cache;
            _decorated = decorated;
        }

        public IReadOnlyList<Account> GetAccountsForCustomer(int userId)
        {
            var accounts = _cache.GetOrAdd("accounts_" + userId,
                () => _decorated.GetAccountsForCustomer(userId));

            return accounts;
        }

        public Account GetAccountForCustomer(int customerId, int accountId)
        {
            return GetAccountsForCustomer(customerId).Single(a => a.Id == accountId);
        }

        public void Update(Account account)
        {
            _decorated.Update(account);
        }

        public void Add(Account account)
        {
            _decorated.Add(account);
        }
    }
}