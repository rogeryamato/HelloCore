using HelloCore.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WebExtension
{
    public static class StartupExtension
    {
        public static void SetupCookie(this IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CommonConstant.CookieName)
            .AddCookie(option =>
                {
                    option.LoginPath = "/Login/Index";
                    option.AccessDeniedPath = "/Login/Index";
                    //option.EventsType = typeof(AuthenticationEvents);
                }
                );

            services.AddAuthorization(option =>
            {
                option.AddPolicy("admin", p =>
                {
                    p.RequireClaim(ClaimTypes.Role, "1");
                   
                });
            });
        }
    }
}
