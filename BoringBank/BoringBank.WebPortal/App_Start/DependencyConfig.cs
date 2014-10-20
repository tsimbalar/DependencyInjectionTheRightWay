using System.Configuration;
using BoringBank.Business;
using BoringBank.Data;
using Microsoft.Practices.Unity;

namespace BoringBank.WebPortal
{
    public class DependencyConfig
    {

        public static void Configure(IUnityContainer container)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BankingDbContext"]
                    .ConnectionString;
            container.RegisterType<IAccountRepository, AccountRepository>(
                new InjectionConstructor(connectionString));
            container.RegisterType<IUserAccountService, UserAccountService>();
        }

    }

}