using System.Linq;
using System.Web.Mvc;
using BoringBank.WebPortal.Data;

namespace BoringBank.WebPortal.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            var userId = this.User.AsClaimsPrincipal().UserId();

            using (var context = new BankingDbContext())
            {
                var accounts = context.Accounts
                    .Where(a => a.CustomerId == userId)
                    .OrderBy(a => a.Title).ToList();

                return View(accounts);
            }
        }

        [HttpPost]
        public ActionResult Rename(int id, string newName)
        {
            var userId = this.User.AsClaimsPrincipal().UserId();

            using (var context = new BankingDbContext())
            {
                var accountToUpdate = context.Accounts
                    .Single(a => a.CustomerId == userId && a.Id == id);

                accountToUpdate.Title = newName;

                context.SaveChanges();

                // done ... redirect ...
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Add(string name)
        {
            var userId = this.User.AsClaimsPrincipal().UserId();

            using (var context = new BankingDbContext())
            {
                var newAccount = new Account()
                                 {
                                     CustomerId = userId,
                                     Balance = 0.0m,
                                     Title = name
                                 };
                context.Accounts.Add(newAccount);

                context.SaveChanges();

                // done ... redirect ...
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Transfer()
        {
            var userId = this.User.AsClaimsPrincipal().UserId();

            using (var context = new BankingDbContext())
            {
                var allUserAccounts = context.Accounts
                    .Where(a => a.CustomerId == userId)
                    .OrderBy(a => a.Title).ToList();

                return View(allUserAccounts);
            }
        }

        [HttpPost]
        public ActionResult TransferPost(int from, int to, decimal amount)
        {
            var userId = this.User.AsClaimsPrincipal().UserId();

            using (var context = new BankingDbContext())
            {
                var accountFrom = context.Accounts
                    .Single(a => a.CustomerId == userId && a.Id == from);
                var accountTo = context.Accounts
                    .Single(a => a.CustomerId == userId && a.Id == to);

                accountFrom.Balance -= amount;
                accountTo.Balance += amount;

                context.SaveChanges();

                return RedirectToAction("Index");
            }

        }


    }
}