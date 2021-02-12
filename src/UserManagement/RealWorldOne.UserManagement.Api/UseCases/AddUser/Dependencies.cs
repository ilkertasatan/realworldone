using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RealWorldOne.UserManagement.Domain;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Infrastructure.DataAccess;
using RealWorldOne.UserManagement.Infrastructure.DataAccess.Repositories;

namespace RealWorldOne.UserManagement.Api.UseCases.AddUser
{
    public static class Dependencies
    {
        public static IServiceCollection AddUserUseCase(this IServiceCollection services)
        {
            services.TryAddSingleton<IUserFactory, EntityFactory>();
            services.TryAddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}