using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using BoringBank.Business;
using BoringBank.Data;
using BoringBank.WebPortal.Controllers;

namespace BoringBank.WebPortal
{
    public class AppCompositionRoot : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // how to compose an AccountController ?
            if (controllerType == typeof(AccountController))
            {
                var connectionString = ConfigurationManager.ConnectionStrings["BankingDbContext"]
                    .ConnectionString;

                var repo = new AccountRepository(connectionString);

                var service = new UserAccountService(repo);

                return new AccountController(service);
            }

            // standard way in MVC to use default strategy
            return null;
        }
    }
}
