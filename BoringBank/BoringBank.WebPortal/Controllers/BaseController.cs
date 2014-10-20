using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BoringBank.WebPortal.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.HttpContext.User = new ClaimsPrincipal(
                 new ClaimsIdentity(new Claim[]
                                   {
                                       new Claim(ClaimTypes.NameIdentifier, "1", ClaimValueTypes.Integer32),
                                       new Claim(ClaimTypes.Email, "tibo.desodt@gmail.com"),
                                       new Claim(ClaimTypes.Name, "tsimbalar"),
                                       new Claim(ClaimTypes.GivenName, "Thibaud"),
                                       new Claim(ClaimTypes.Surname, "Desodt"),
                                   }));
            base.OnActionExecuting(filterContext);
        }
    }
}