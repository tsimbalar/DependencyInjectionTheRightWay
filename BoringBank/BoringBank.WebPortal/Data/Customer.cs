using System.Collections.Generic;

namespace BoringBank.WebPortal.Data
{
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
}