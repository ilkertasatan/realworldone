using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RealWorldOne.UserManagement.Application.Common.Security;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;
using RealWorldOne.UserManagement.Infrastructure.DataAccess.Repositories;
using Xunit;

namespace RealWorldOne.KittenGenerator.IntegrationTests.InfrastructureTests.DataAccess.Repositories
{
    public class UserRepositoryShould : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        public UserRepositoryShould(DatabaseFixture fixture)
        {
            _fixture = fixture;

            var cancellation = new CancellationTokenSource();
            cancellation.CancelAfter(TimeSpan.FromSeconds(10));
            
            _cancellationToken = cancellation.Token;
        }

        [Fact]
        public async Task Save_User()
        {
            var expectedUser = new User(
                UserId.NewUserId(),
                new Name("name"),
                new Email("user@email.com"),
                new Password("pass"),
                new PasswordSalt(Salt.Create()));
            var sut = new UserRepository(_fixture.UserManagementDataContext);

            await sut.SaveAsync(expectedUser, _cancellationToken);
            await sut.CommitChangesAsync(_cancellationToken);

            var actualUser = await sut.SelectByEmailAsync(expectedUser.Email, _cancellationToken);
            actualUser.Should().BeEquivalentTo(expectedUser);
        }
    }
}