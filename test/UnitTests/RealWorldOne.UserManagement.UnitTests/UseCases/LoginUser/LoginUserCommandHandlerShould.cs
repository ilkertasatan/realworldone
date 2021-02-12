using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Moq;
using RealWorldOne.UserManagement.Application.Common.Behaviours;
using RealWorldOne.UserManagement.Application.UseCases.LoginUser;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;
using RealWorldOne.UserManagement.Infrastructure.Security;
using Xunit;
using ValidationException = RealWorldOne.UserManagement.Application.Common.Exceptions.ValidationException;

namespace RealWorldOne.UserManagement.UnitTests.UseCases.LoginUser
{
    public class LoginUserCommandHandlerShould
    {
        private readonly LoginUserCommandHandler _sut;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IGenerateAccessToken> _tokenGeneratorMock;
        
        public LoginUserCommandHandlerShould()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _tokenGeneratorMock = new Mock<IGenerateAccessToken>();
            
            var optionsMock = new Mock<IOptions<AuthenticationSettings>>();
            optionsMock
                .SetupGet(x => x.Value)
                .Returns(new AuthenticationSettings
                {
                    Secret = "secret",
                    Aud = "aud",
                    Iss = "iss"
                });
            
            _sut = new LoginUserCommandHandler(_userRepositoryMock.Object, optionsMock.Object, _tokenGeneratorMock.Object);
        }

        [Fact]
        public async Task Log_In_User_When_User_Registered()
        {
            var expectedResult = new AccessToken("access-token", 100);
            _userRepositoryMock
                .Setup(x => x.SelectByEmailAndPasswordAsync(
                    It.IsAny<Email>(),
                    It.IsAny<Password>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new User(UserId.NewUserId(), new Name("name"),new Email("user@email.com"),new Password("password")));
            _tokenGeneratorMock
                .Setup(x => x.CreateAccessToken(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(expectedResult);
            
            var actualResult = await _sut.Handle(new LoginUserCommand("user@email.com", "password"), CancellationToken.None);

            actualResult.Should()
                .BeOfType<LoginUserCommandResult>()
                .Which.AccessToken
                .Should().Be(expectedResult.Token);
        }

        [Fact]
        public async Task Return_User_Not_Found_When_User_DoesNot_Exist()
        {
            _userRepositoryMock
                .Setup(x => x.SelectByEmailAndPasswordAsync(
                    It.IsAny<Email>(),
                    It.IsAny<Password>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(User.None);
            
            var actualResult = await _sut.Handle(new LoginUserCommand("user@email.com", "password"), CancellationToken.None);

            actualResult.Should().BeOfType<UserNotFoundResult>();
        }
        
        [Theory]
        [InlineData(null, "pass")]
        [InlineData("", "pass")]
        [InlineData("user@email.com", null)]
        [InlineData("user@email.com", "")]
        [InlineData("invalid-email", "pass")]
        public void Throw_Exception_When_Validation_Failures_Occurred(string email, string password)
        {
            var expectedCommand = new LoginUserCommand(email, password);
            var validators = new List<IValidator<LoginUserCommand>> {new LoginUserCommandValidator()};
            var requestDelegateMock = new Mock<RequestHandlerDelegate<LoginUserCommandResult>>();
            var sut = new RequestValidatorBehavior<LoginUserCommand, LoginUserCommandResult>(validators);
            
            Action act = () =>
            {
                sut.Handle(expectedCommand, CancellationToken.None, requestDelegateMock.Object);
            };

            act.Should().ThrowExactly<ValidationException>();
        }
    }
}