﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RealWorldOne.UserManagement.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "RealWorldOne.UserManagement.Api", Version = "v1"});
            });
            
            services.AddSwaggerGenNewtonsoftSupport();
            
            return services;
        }
    }
}