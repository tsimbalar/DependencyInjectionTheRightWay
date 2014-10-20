using System.Collections.Generic;
using System.Data.Entity;

namespace BoringBank.Data
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(string connectionString)
            : base("BankingDbContext") 
        {
            Database.SetInitializer<BankingDbContext>(new BankingDbInitializer());
        }

        public IDbSet<Account> Accounts { get; set; }
        public IDbSet<Customer> Customers { get; set; }
    }

    public class BankingDbInitializer : DropCreateDatabaseIfModelChanges<BankingDbContext>
    {
        protected override void Seed(BankingDbContext context)
        {
            var customer = new Customer()
                           {
                               FirstName = "Thibaud",
                               LastName = "Desodt",
                               Accounts = new List<Account>()
                                          {
                                              new Account()
                                              {
                                                  Title = "Savings Account",
                                                  Balance = 1423.45M,

                                              },
                                              new Account
                                              {
                                                  Title = "Hidden Caiman Islands Account",
                                                  Balance = 900000000.0M
                                              }
                                          }
                           };

            context.Customers.Add(customer);

            base.Seed(context);
        }
    }
}