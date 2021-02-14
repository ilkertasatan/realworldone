using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealWorldOne.UserManagement.Application.Common.Exceptions;

namespace RealWorldOne.UserManagement.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    var statusCode = StatusCodes.Status500InternalServerError;
                    object errorResult;

                    if (exception is ValidationException validationException)
                    {
                        var errors = validationException.Failures.Select(e => e.ErrorMessage);
                        errorResult = new
                        {
                            status = StatusCodes.Status400BadRequest,
                            message = exception.Message,
                            errors
                        };

                        statusCode = StatusCodes.Status400BadRequest;
                    }
                    else
                    {
                        errorResult = new ProblemDetails
                        {
                            Status = context.Response.StatusCode,
                            Title = "An error occurred"
                        };
                    }

                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResult,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        }), Encoding.UTF8);
                });
            });

            return app;
        }
    }
}