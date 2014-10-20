using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BoringBank.WebPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var factory = new AppCompositionRoot();
            ControllerBuilder.Current.SetControllerFactory(factory);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
