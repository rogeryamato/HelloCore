using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using HelloCore.Interface.Manager;
using HelloCore.DomainModel;
using Microsoft.AspNetCore.Mvc;

namespace WebExtension
{
    public class HelloCoreAuthorizeAttribute : TypeFilterAttribute
    {
        public HelloCoreAuthorizeAttribute():base(typeof(HelloCoreAuthorizeFilter))
        {

        }
           
    }

    public class HelloCoreAuthorizeFilter : AuthorizeFilter
    {

        private readonly IUserManager userManager;

        public HelloCoreAuthorizeFilter (IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public override async System.Threading.Tasks.Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var id = context.HttpContext.User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
            if (id != null && !string.IsNullOrWhiteSpace(id.Value))
            {
                var user = GetUser(Convert.ToInt32(id.Value));
                var controller = context.RouteData.Values["controller"];
                var action = context.RouteData.Values["action"];

                await user;
                if (IsAuthorize(controller.ToString(), action.ToString(), user.Result.Entity) == false)
                    context.Result = new RedirectToActionResult("index", "login",null);
            }
            else
                context.Result = new RedirectToActionResult("index", "login", null);
        }

       private async Task<ResultEntity< User>> GetUser(int id)
        {
            return await userManager.GetAsync(id);
        }

        private bool IsAuthorize(string controller, string action, User user)
        {
            if (user.UserId == 1 && controller == "Task")
            {
                return true;
            }
            else if (user.UserId == 2 && controller == "Home" && action == "Contact")
            {
                return true;
            }
            return false;
        }
    }
}
