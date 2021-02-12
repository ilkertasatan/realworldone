using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;
using RealWorldOne.UserManagement.Application.Common.Behaviours;
using RealWorldOne.UserManagement.Application.UseCases.AddUser;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;
using RealWorldOne.UserManagement.Infrastructure.DataAccess;
using Xunit;
using ValidationException = RealWorldOne.UserManagement.Application.Common.Exceptions.ValidationException;

namespace RealWorldOne.UserManagement.UnitTests.UseCases.AddUser
{
    public class AddUserCommandHandlerShould
    {
        private readonly AddUserCommandHandler _sut;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public AddUserCommandHandlerShould()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _sut = new AddUserCommandHandler(_userRepositoryMock.Object, new EntityFactory());
        }
        
        [Fact]
        public async Task Add_New_User_When_User_DoesNot_Exist()
        {
            var expectedUser =
                new EntityFactory().NewUser(new Name("user-name"), new Email("email"), new Password("pass"));
            _userRepositoryMock
                .Setup(x => x.SaveAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);
            _userRepositoryMock
                .Setup(x => x.SelectByEmailAsync(expectedUser.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(User.None);

            var actualResult = await _sut.Handle(new AddUserCommand("user-name", "email", "pass"), CancellationToken.None);
                
            actualResult.Should()
                .BeOfType<AddUserCommandResult>()
                .Which.User
                .Should().BeEquivalentTo(expectedUser);
            _userRepositoryMock.Verify(x => x.SelectByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(x => x.SaveAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Ensure_Idempotency_Given_Registered_User()
        {
            var expectedUser =
                new EntityFactory().NewUser(new Name("user-name"), new Email("email"), new Password("pass"));
            _userRepositoryMock
                .Setup(x => x.SelectByEmailAsync(expectedUser.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);

            var actualResult = await _sut.Handle(new AddUserCommand("user-name", "email", "pass"), CancellationToken.None);
                
            actualResult.Should()
                .BeOfType<AddUserCommandResult>()
                .Which.User
                .Should().BeEquivalentTo(expectedUser);
            _userRepositoryMock.Verify(x => x.SelectByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(x => x.SaveAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never); 
        }

        [Theory]
        [InlineData(null, "user@email.com", "pass")]
        [InlineData("", "user@email.com", "pass")]
        [InlineData("name", null, "pass")]
        [InlineData("name", "", "pass")]
        [InlineData("name", "user@email.com", null)]
        [InlineData("name", "user@email.com", "")]
        [InlineData("name", "invalid-email", "pass")]
        public void Throw_Exception_When_Validation_Failures_Occurred(string name, string email, string password)
        {
            var expectedCommand = new AddUserCommand(name, email, password);
            var validators = new List<IValidator<AddUserCommand>> {new AddUserCommandValidator()};
            var requestDelegateMock = new Mock<RequestHandlerDelegate<AddUserCommandResult>>();
            var sut = new RequestValidatorBehavior<AddUserCommand, AddUserCommandResult>(validators);
            
            Action act = () =>
            {
                sut.Handle(expectedCommand, CancellationToken.None, requestDelegateMock.Object);
            };

            act.Should().ThrowExactly<ValidationException>();
        }
    }
}