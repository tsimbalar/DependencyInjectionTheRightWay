using System.Collections.Generic;
using System.Data.Entity;

namespace BoringBank.WebPortal.Data
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext()
            : base("BankingDbContext") 
        {
            Database.SetInitializer<BankingDbContext>(new BankingDbInitializer());
        }

        public IDbSet<Account> Accounts { get; set; }
        public IDbSet<Customer> Customers { get; set; }
    }

    public class Account
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string Title { get; set; }
        public decimal Balance { get; set; }
    }

    public class Customer
    {
        public Customer()
        {
            Accounts = new List<Account>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Account> Accounts { get; set; } 
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