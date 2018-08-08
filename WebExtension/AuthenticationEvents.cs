using HelloCore.Common;
using HelloCore.Interface.Manager;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebExtension
{
    /// <summary>
    /// define authentication check policy, this is not needed if no special demand.
    /// </summary>
    public class AuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IUserManager userManager;

        public AuthenticationEvents(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            // this is defualt check critiria.
            //context.Principal.Identity.IsAuthenticated
            var userPrincipal = context.Principal;
            var userId = userPrincipal.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                context.RejectPrincipal();

                await context.HttpContext.SignOutAsync(CommonConstant.CookieName);
            }
            
        }
    }
}
