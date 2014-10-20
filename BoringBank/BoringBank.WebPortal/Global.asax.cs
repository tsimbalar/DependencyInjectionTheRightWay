using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BoringBank.WebPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //this.PreRequestHandlerExecute += MvcApplication_PreRequestHandlerExecute;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void MvcApplication_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            this.Context.User = new ClaimsPrincipal(
                new ClaimsIdentity(new []
                                   {
                                       new Claim(ClaimTypes.NameIdentifier, "1", ClaimValueTypes.Integer32),
                                       new Claim(ClaimTypes.Email, "tibo.desodt@gmail.com"),
                                       new Claim(ClaimTypes.Name, "tsimbalar"),
                                       new Claim(ClaimTypes.GivenName, "Thibaud"),
                                       new Claim(ClaimTypes.Surname, "Desodt"),
                                   }));
        }

        //void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        //{

        //    this.Context.User = new ClaimsPrincipal(
        //         new ClaimsIdentity(new Claim[]
        //                           {
        //                               new Claim(ClaimTypes.NameIdentifier, "1", ClaimValueTypes.Integer32),
        //                               new Claim(ClaimTypes.Email, "tibo.desodt@gmail.com"),
        //                               new Claim(ClaimTypes.Name, "tsimbalar"),
        //                               new Claim(ClaimTypes.GivenName, "Thibaud"),
        //                               new Claim(ClaimTypes.Surname, "Desodt"),
        //                           }));
        //}




    }
}
