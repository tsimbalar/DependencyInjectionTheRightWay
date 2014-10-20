using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BoringBank.Business;
using BoringBank.Business.Domain;
using BoringBank.WebPortal.Models;

namespace BoringBank.WebPortal.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            var userId = this.User.AsClaimsPrincipal().UserId();

            var userAccountService = new UserAccountService();
            var accounts = userAccountService.GetAccountsForCustomer(userId);

            return View(ToViewModel(accounts));
        }


        [HttpPost]
        public ActionResult Rename(int id, string newName)
        {
            var userId = this.User.AsClaimsPrincipal().UserId();

            var userAccountService = new UserAccountService();
            userAccountService.RenameAccount(userId, id, newName);

            // done ... redirect ...
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(string name)
        {
            var userId = this.User.AsClaimsPrincipal().UserId();
            var userAccountService = new UserAccountService();
            userAccountService.CreateAccountForCustomer(userId, name);

            // done ... redirect ...
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Transfer()
        {
            var userId = this.User.AsClaimsPrincipal().UserId();

            var userAccountService = new UserAccountService();
            var allUserAccounts = userAccountService.GetAccountsForCustomer(userId);

            return View(ToViewModel(allUserAccounts));
        }

        [HttpPost]
        public ActionResult TransferPost(int from, int to, decimal amount)
        {
            var userId = this.User.AsClaimsPrincipal().UserId();

            var userAccountService = new UserAccountService();
            userAccountService.Transfer(userId, from, to, amount);

            return RedirectToAction("Index");
        }


        private UserAccountListViewModel ToViewModel(IEnumerable<Account> accounts)
        {
            return new UserAccountListViewModel()
            {
                Accounts = accounts.Select(a => new UserAccountViewModel()
                {
                    Name = a.Name,
                    Id = a.Id,
                    Balance = a.Balance
                }).ToList().AsReadOnly()
            };
        }


    }
}