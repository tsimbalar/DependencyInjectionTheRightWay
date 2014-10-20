using System.Configuration;
using BoringBank.Business;
using BoringBank.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace BoringBank.WebPortal
{
    public class DependencyConfig
    {
        public static void Configure(IUnityContainer container)
        {
            container.AddNewExtension<Interception>();
            var connectionString = ConfigurationManager.ConnectionStrings["BankingDbContext"]
                    .ConnectionString;
            container.RegisterType<IAccountRepository, AccountRepository>(
                new InjectionConstructor(connectionString),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<TimingBehavior>());
            container.RegisterType<IUserAccountService, UserAccountService>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<TimingBehavior>());
        }
    }
}