using System.Collections.Generic;
using System.Linq;

namespace BoringBank.Data
{
    public class AccountRepository
    {
        public IReadOnlyList<Account> GetAccountsForCustomer(int userId)
        {
            using (var context = new BankingDbContext("BankingDbContext"))
            {
                var accounts = Queryable.Where<Account>(context.Accounts, a => a.CustomerId == userId)
                    .OrderBy(a => a.Title).ToList();

                return accounts.AsReadOnly();
                //return accounts.Select(ToDomain).ToList().AsReadOnly();

            }
        }

        public Account GetAccountForCustomer(int customerId, int accountId)
        {
            using (var context = new BankingDbContext("BankingDbContext"))
            {
                var account = context.Accounts
                    .Single(a => a.CustomerId == customerId && a.Id == accountId);

                return account;
            }
        }

        public void Update(Account account)
        {
            using (var context = new BankingDbContext("BankingDbContext"))
            {
                var accountEf = context.Accounts.Find(account.Id);
                // theoretically, could do "if not changed"
                accountEf.Balance = account.Balance;
                accountEf.Title = account.Title;

                context.SaveChanges();
            }
        }

        public void Add(Account account)
        {
            using (var context = new BankingDbContext("BankingDbContext"))
            {
                //var accountToAdd = NewFromDomain(account);
                context.Accounts.Add(account);
                context.SaveChanges();
            }
        }

        //private static Domain.Account ToDomain(Account ac)
        //{
        //    return new Domain.Account
        //           {
        //               Balance = ac.Balance,
        //               CustomerId = ac.CustomerId,
        //               Id = ac.Id,
        //               Name = ac.Title
        //           };
        //}

        //private static Account NewFromDomain(Domain.Account ac)
        //{
        //    return new Account()
        //           {
        //               Balance = ac.Balance,
        //               CustomerId = ac.CustomerId,
        //               Title = ac.Name
        //           };
        //}
    }
}