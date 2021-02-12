using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealWorldOne.UserManagement.Api.UseCases.AddUser;
using RealWorldOne.UserManagement.Application.UseCases.AddUser;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;
using RealWorldOne.UserManagement.Infrastructure.DataAccess;
using Xunit;

namespace RealWorldOne.UserManagement.UnitTests.UseCases.AddUser
{
    public class UserControllerShould
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UserController _sut;

        public UserControllerShould()
        {
            _mediatorMock = new Mock<IMediator>();
            _sut = new UserController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Return_201_When_User_Registered()
        {
            var expectedUser =
                new EntityFactory().NewUser(new Name("user-name"), new Email("email"), new Password("pass"));
            var expectedResponse = new AddUserResponse
            {
                UserId = expectedUser.Id.Value,
                Name = expectedUser.Name.Value,
                Email = expectedUser.Email.Value
            };
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AddUserCommandResult(expectedUser));

            var actualResult = await _sut.AddNewUser(
                new AddUserRequest
                {
                    Name = "user-name",
                    Email = "email",
                    Password = "pass"
                });

            actualResult.Should()
                .BeOfType<CreatedResult>()
                .Which.Value
                .Should()
                .BeEquivalentTo(expectedResponse);
            _mediatorMock.Verify(x => x.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}