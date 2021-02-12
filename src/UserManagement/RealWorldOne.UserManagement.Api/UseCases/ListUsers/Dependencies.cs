using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Infrastructure.DataAccess.Repositories;

namespace RealWorldOne.UserManagement.Api.UseCases.ListUsers
{
    public static class Dependencies
    {
        public static IServiceCollection AddListUsersUseCase(this IServiceCollection services)
        {
            services.TryAddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}