using Microsoft.EntityFrameworkCore;
using RealWorldOne.UserManagement.Domain.Users;

namespace RealWorldOne.UserManagement.Infrastructure.DataAccess
{
    public class UserManagementDataContext : DbContext
    {
        public UserManagementDataContext(DbContextOptions<UserManagementDataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementDataContext).Assembly);
        }
    }
}