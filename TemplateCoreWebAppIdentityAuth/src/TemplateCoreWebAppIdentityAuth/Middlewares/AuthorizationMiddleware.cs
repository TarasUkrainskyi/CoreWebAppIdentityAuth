using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.IO;

namespace TemplateCoreWebAppIdentityAuth.Middlewares
{
    public class AuthorizationMiddleware
    {
        RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains("X-Not-Authorized"))
            {
                context.Response.StatusCode = 401; //Unauthorized
                return;
            }

            await _next.Invoke(context);
        }
    }    
}
