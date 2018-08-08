using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SpaServices.Webpack;

namespace WebExtension
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseRequestCookie(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCookieMiddleware>();
        }

        public static void UseMyWebpack(this IApplicationBuilder builder)
        {
            builder.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions()
            {
                HotModuleReplacement = true
            });
        }
    }
}
