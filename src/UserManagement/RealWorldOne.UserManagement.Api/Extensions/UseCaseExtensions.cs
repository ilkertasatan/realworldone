using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealWorldOne.UserManagement.Api.UseCases.AddUser;
using RealWorldOne.UserManagement.Api.UseCases.ListUsers;
using RealWorldOne.UserManagement.Api.UseCases.LoginUser;

namespace RealWorldOne.UserManagement.Api.Extensions
{
    public static class UseCaseExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddUserUseCase()
                .AddLoginUserUseCase(configuration)
                .AddListUsersUseCase();
            
            return services;
        }
    }
}