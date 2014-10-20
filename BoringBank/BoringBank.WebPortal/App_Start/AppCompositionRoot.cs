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

                var nakedRepo = new AccountRepository(connectionString);

                // decorate the nakedRepository with caching features
                var longCache = new DotNetCache(TimeSpan.FromHours(1));
                var cachedRepo = new CachedAccountRepository(longCache, nakedRepo);
                var service = new UserAccountService(cachedRepo);

                return new AccountController(service);
            }

            // standard way in MVC to use default strategy
            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}
