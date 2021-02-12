using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RealWorldOne.UserManagement.Application.UseCases.AddUser;

namespace RealWorldOne.UserManagement.Api.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            AssemblyScanner
                .FindValidatorsInAssembly(typeof(AddUserCommand).Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            return services;
        }
    }
}