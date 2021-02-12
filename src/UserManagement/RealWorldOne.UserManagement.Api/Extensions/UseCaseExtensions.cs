using Microsoft.Extensions.DependencyInjection;
using RealWorldOne.UserManagement.Api.UseCases.AddUser;

namespace RealWorldOne.UserManagement.Api.Extensions
{
    public static class UseCaseExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddUserUseCase();
            return services;
        }
    }
}