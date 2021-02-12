using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RealWorldOne.UserManagement.Application.Common.Behaviours;
using RealWorldOne.UserManagement.Application.UseCases.AddUser;

namespace RealWorldOne.UserManagement.Api.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AddUserCommand).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidatorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
            
            return services;
        }
    }
}