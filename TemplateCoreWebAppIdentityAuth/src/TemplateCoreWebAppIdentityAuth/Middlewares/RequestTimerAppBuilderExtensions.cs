using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateCoreWebAppIdentityAuth.Middlewares
{
    public static class RequestTimerAppBuilderExtensions
    {
        public static IApplicationBuilder UseRequestTimer(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<RequestTimerMiddleware>();
        }        
    }
}
