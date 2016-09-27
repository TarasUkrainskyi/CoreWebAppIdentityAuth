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
    public class ProcessingTimeMiddleware
    {
        RequestDelegate _next;

        public ProcessingTimeMiddleware(RequestDelegate next)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            _next = next;
        }

        //can add options, handlers etc

        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();

            using (var buffer = new MemoryStream())
            {
                // Buffer the response
                var bodyStream = context.Response.Body;
                context.Response.Body = buffer;

                await _next(context);

                var isHtml = context.Response.ContentType?.ToLower().Contains("text/html");
                if (context.Response.StatusCode == 200 && isHtml.GetValueOrDefault())
                {
                    {
                        buffer.Seek(0, SeekOrigin.Begin);
                        string responseBody = string.Empty;

                        using (var streamReader = new StreamReader(buffer))
                        {
                            responseBody = await streamReader.ReadToEndAsync();
                            string elapsedTime = sw.ElapsedMilliseconds.ToString();
                            responseBody = responseBody.Replace("{0}", elapsedTime);
                            context.Response.Headers.Add("X-ElapsedTime", new[] { elapsedTime });

                            using (var streamWriter = new StreamWriter(bodyStream))
                            {
                                await streamWriter.WriteAsync(responseBody);
                            }
                        }                       
                    }
                }
            }
        }
    }    
}
