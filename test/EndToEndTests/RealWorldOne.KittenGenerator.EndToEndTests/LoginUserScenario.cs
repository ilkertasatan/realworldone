using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RealWorldOne.UserManagement.Api.UseCases.AddUser;
using RealWorldOne.UserManagement.Api.UseCases.LoginUser;
using Xbehave;
using Xunit;

namespace RealWorldOne.KittenGenerator.EndToEndTests
{
    public class LoginUserScenario : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;
        private readonly CancellationTokenSource _cancellation;

        public LoginUserScenario(TestServerFixture fixture)
        {
            _cancellation = new CancellationTokenSource();
            _cancellation.CancelAfter(TimeSpan.FromSeconds(30));

            _fixture = fixture;
        }
        
        [Scenario]
        public void Login_User(LoginUserResponse loginResponse)
        {
            "Given I have an user".x(async () => await GivenUserAsync());
            "When user logged in".x(async () => loginResponse = await LoginUserAsync());
            "Then access token is generated".x(
                () =>
                {
                    loginResponse.AccessToken.Should().NotBeNull();
                    loginResponse.ExpiresIn.Should().BeGreaterThan(0);
                });
        }
        
        private async Task GivenUserAsync()
        {
            var request = new AddUserRequest
            {
                Name = "user-name",
                Email = "user@email.com",
                Password = "password"
            };
            var response = await _fixture
                .PostAsync<AddUserResponse, AddUserRequest>("/api/users", request, _cancellation.Token);
        }

        private async Task<LoginUserResponse> LoginUserAsync()
        {
            var request = new LoginUserRequest
            {
                Email = "user@email.com",
                Password = "password"
            };

            var response = await _fixture
                .PostAsync<LoginUserResponse, LoginUserRequest>("/api/users/login", request, _cancellation.Token);
            
            return response;
        }
    }
}