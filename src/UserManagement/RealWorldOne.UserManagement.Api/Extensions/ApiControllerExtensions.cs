using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RealWorldOne.UserManagement.Api.Extensions
{
    public static class ApiControllerExtensions
    {
        public static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(config =>
                {
                    config.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new BadRequestObjectResult(context.ModelState);
                        
                        result.ContentTypes.Add(MediaTypeNames.Application.Json);

                        return result;
                    };
                });

            return services;
        }
    }
}