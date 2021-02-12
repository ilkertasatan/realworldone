using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealWorldOne.UserManagement.Api.UseCases.LoginUser;
using RealWorldOne.UserManagement.Application.UseCases.LoginUser;
using Xunit;

namespace RealWorldOne.UserManagement.UnitTests.UseCases.LoginUser
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
        public async Task Return_200_When_User_Logged_In_Successfully()
        {
            var expectedResult = new LoginUserResponse
            {
                AccessToken = "access-token",
                ExpiresIn = 100
            };
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new LoginUserCommandResult(expectedResult.AccessToken, expectedResult.ExpiresIn));
            
            var actualResult = await _sut.LoginUserAsync(new LoginUserRequest
            {
                Email = "user@email.com",
                Password = "password"
            });
            
            actualResult.Should().BeOfType<OkObjectResult>()
                .Which.Value
                .Should().BeEquivalentTo(expectedResult);
            _mediatorMock.Verify(x => x.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Return_404_When_User_DoesNot_Exist()
        {
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new UserNotFoundResult(""));
            
            var actualResult = await _sut.LoginUserAsync(new LoginUserRequest
            {
                Email = "user@email.com",
                Password = "password"
            });

            actualResult.Should().BeOfType<NotFoundObjectResult>();
            _mediatorMock.Verify(x => x.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}