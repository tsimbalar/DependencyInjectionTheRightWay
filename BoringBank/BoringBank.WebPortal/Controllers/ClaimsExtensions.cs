using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace BoringBank.WebPortal.Controllers
{
    public static class ClaimsExtensions
    {

        public static ClaimsPrincipal AsClaimsPrincipal(this IPrincipal self)
        {
            return (ClaimsPrincipal)self;
        }

        public static int UserId(this ClaimsPrincipal self)
        {
            return Int32.Parse(
                self.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}