using System.Collections.Generic;

namespace BoringBank.WebPortal.Models
{
    public class UserAccountListViewModel
    {
        public UserAccountListViewModel()
        {
            Accounts = new List<UserAccountViewModel>();
        }

        public IReadOnlyList<UserAccountViewModel> Accounts { get; set; }
    }
}