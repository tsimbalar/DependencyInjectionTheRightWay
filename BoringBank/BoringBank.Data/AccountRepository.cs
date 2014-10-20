using System.Collections.Generic;
using System.Linq;
using BoringBank.Business;

namespace BoringBank.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Business.Domain.Account> GetAccountsForCustomer(int userId)
        {
            using (var context = new BankingDbContext(_connectionString))
            {
                var accounts = context.Accounts.Where(a => a.CustomerId == userId)
                    .OrderBy(a => a.Title).ToList();
                return accounts.Select(ToDomain).ToList().AsReadOnly();

            }
        }

        public Business.Domain.Account GetAccountForCustomer(int customerId, int accountId)
        {
            using (var context = new BankingDbContext(_connectionString))
            {
                var account = context.Accounts
                    .Single(a => a.CustomerId == customerId && a.Id == accountId);

                return ToDomain(account);
            }
        }

        public void Update(Business.Domain.Account account)
        {
            using (var context = new BankingDbContext(_connectionString))
            {
                var accountEf = context.Accounts.Find(account.Id);
                // theoretically, could do "if not changed"
                accountEf.Balance = account.Balance;
                accountEf.Title = account.Name;

                context.SaveChanges();
            }
        }

        public void Add(Business.Domain.Account account)
        {
            using (var context = new BankingDbContext(_connectionString))
            {
                var accountToAdd = NewFromDomain(account);
                context.Accounts.Add(accountToAdd);
                context.SaveChanges();
            }
        }

        private static Business.Domain.Account ToDomain(Account ac)
        {
            return new Business.Domain.Account
                   {
                       Balance = ac.Balance,
                       CustomerId = ac.CustomerId,
                       Id = ac.Id,
                       Name = ac.Title
                   };
        }

        private static Account NewFromDomain(Business.Domain.Account ac)
        {
            return new Account()
                   {
                       Balance = ac.Balance,
                       CustomerId = ac.CustomerId,
                       Title = ac.Name
                   };
        }
    }
}