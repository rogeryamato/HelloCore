using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HelloCore.Common;
using HelloCore.DomainModel;
using HelloCore.DomainModel.Models;
using HelloCore.DomainModel.ViewModels;
using HelloCore.Interface.Manager;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebExtension;

namespace HelloCore.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        private readonly IUserManager userManager;

        public LoginController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.SignOutAsync(CommonConstant.CookieName);

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel model)
        {
            var result = Validate(model);

            if (result.IsSuccess)
            {
                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.Email,result.Entity.Email),
                    new Claim(ClaimTypes.Role,result.Entity.UserRoleId.ToString()),
                    new Claim(ClaimTypes.Name,result.Entity.UserName),
                    new Claim(ClaimTypes.NameIdentifier,result.Entity.UserId.ToString())
                };

                var claimIdentity = new ClaimsIdentity(claims, CommonConstant.CookieName);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    //IsPersistent = true
                };

                var principal = new HelloCorePrincipal(claimIdentity);
                HttpContext.SignInAsync(CommonConstant.CookieName, principal, authProperties);
                HttpContext.User = principal;
                return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Error);
                return View("Index");
            }
            
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync(CommonConstant.CookieName);
            return RedirectToAction("Index");
        }

        private ResultEntity<User> Validate(UserLoginModel model)
        {
            return  userManager.ValidateUser(model.Email, model.Password);
        }
    }
}