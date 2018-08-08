using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks;

namespace WebExtension
{
    public class RequestCookieMiddleware 
    {
        private readonly RequestDelegate next;

        public RequestCookieMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var etag = context.Request.Cookies["etag"];
            if(string.IsNullOrWhiteSpace(etag))
            {
                context.Response.Cookies.Append("etag", "DirectTypeIn");
            }

            await next(context);
        }

    }
}
