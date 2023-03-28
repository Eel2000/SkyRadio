using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using SkyRadio.Domain.Commons;
using System.Net;

namespace SkyRadio.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response<object>(contextFeature.Error.Message)));
                    }
                });
            });
        }
    }
}
