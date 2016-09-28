using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateCoreWebAppIdentityAuth.Middlewares
{
    public static class MiddlewareAppBuilderExtensions
    {
        public static IApplicationBuilder UseProcessingTimeMiddleware(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ProcessingTimeMiddleware>();
        }

        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<AuthorizationMiddleware>();
        }

        public static IApplicationBuilder UseRequestHeaderMiddleware(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<RequestHeaderMiddleware>();
        }
    }
}
