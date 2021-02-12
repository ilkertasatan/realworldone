using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealWorldOne.UserManagement.Infrastructure.DataAccess;

namespace RealWorldOne.UserManagement.Api.Extensions
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
        {
            services.AddDbContext<UserManagementDataContext>(options => options.UseInMemoryDatabase("UserManagement"));
            return services;
        }
    }
}