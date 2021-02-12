using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RealWorldOne.UserManagement.Api.UseCases.AddUser;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;
using Xbehave;
using Xunit;

namespace RealWorldOne.KittenGenerator.EndToEndTests
{
    public class RegisterNewUserScenario : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;
        private readonly CancellationTokenSource _cancellation;

        public RegisterNewUserScenario(TestServerFixture fixture)
        {
            _cancellation = new CancellationTokenSource();
            _cancellation.CancelAfter(TimeSpan.FromSeconds(30));

            _fixture = fixture;
        }

        [Scenario]
        public void Register_New_User(AddUserRequest request, AddUserResponse response)
        {
            "Given I have an user".x(() => request = GivenUser());
            "When user is registered".x(async () => response = await AddUserAsync(request));
            "Then user is persisted".x(async () => await AssertUserIsPersistedAsync(response));
        }

        private static AddUserRequest GivenUser()
        {
            return new()
            {
                Name = "user-name",
                Email = "user@email.com",
                Password = "password"
            };
        }

        private async Task<AddUserResponse> AddUserAsync(AddUserRequest request)
        {
            var response = await _fixture
                .PostAsync<AddUserResponse, AddUserRequest>("/api/users", request, _cancellation.Token);
            
            return response;
        }
        
        private async Task AssertUserIsPersistedAsync(AddUserResponse response)
        {
            var userRepository = _fixture.TestServer.Services.GetService<IUserRepository>();
            userRepository.Should().NotBeNull();
            
            var persistedUser = await userRepository.SelectByEmailAsync(new Email(response.Email), _cancellation.Token);

            persistedUser.Should().NotBeNull();
            persistedUser.Name.Value.Should().Be(response.Name);
            persistedUser.Email.Value.Should().Be(response.Email);
        }
    }
}