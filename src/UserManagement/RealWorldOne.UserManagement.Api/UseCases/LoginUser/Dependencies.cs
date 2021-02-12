using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Infrastructure.DataAccess.Repositories;
using RealWorldOne.UserManagement.Infrastructure.Security;

namespace RealWorldOne.UserManagement.Api.UseCases.LoginUser
{
    public static class Dependencies
    {
        public static IServiceCollection AddLoginUserUseCase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<AuthenticationSettings>(configuration.GetSection("AuthenticationSettings"));
            services.TryAddScoped<IGenerateAccessToken, AccessTokenGenerator>();
            services.TryAddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}