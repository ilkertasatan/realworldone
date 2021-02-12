using System;
using Microsoft.EntityFrameworkCore;
using RealWorldOne.UserManagement.Infrastructure.DataAccess;

namespace RealWorldOne.KittenGenerator.IntegrationTests.InfrastructureTests.DataAccess
{
    public class DatabaseFixture : IDisposable
    {
        public UserManagementDataContext UserManagementDataContext { get; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<UserManagementDataContext>()
                .UseInMemoryDatabase("UserManagement")
                .Options;

            UserManagementDataContext = new UserManagementDataContext(options);
        }

        public void Dispose()
        {
            UserManagementDataContext?.Dispose();
        }
    }
}