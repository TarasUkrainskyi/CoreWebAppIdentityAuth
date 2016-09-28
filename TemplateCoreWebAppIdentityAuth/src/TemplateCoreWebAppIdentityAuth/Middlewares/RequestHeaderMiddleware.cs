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
    public class RequestHeaderMiddleware
    {
        RequestDelegate _next;

        public RequestHeaderMiddleware(RequestDelegate next)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains("X-Cancel-Request"))
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                return;
            }

            await _next.Invoke(context);

            if (context.Request.Headers.Keys.Contains("X-Transfer-By"))
            {
                context.Response.Headers.Add("X-Transfer-Success", "true");
            }
        }
    }    
}
