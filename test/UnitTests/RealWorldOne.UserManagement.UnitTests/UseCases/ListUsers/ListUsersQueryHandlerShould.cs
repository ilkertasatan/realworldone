using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RealWorldOne.UserManagement.Application.UseCases.ListUsers;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;
using Xunit;

namespace RealWorldOne.UserManagement.UnitTests.UseCases.ListUsers
{
    public class ListUsersQueryHandlerShould
    {
        private readonly Mock<IUserRepository> _repositoryMock;

        public ListUsersQueryHandlerShould()
        {
            _repositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Return_List_Of_Users()
        {
            var expectedUsers = new List<User>
            {
                GivenUser(UserId.NewUserId().Value, "name1", "user1@email.com", "password1"),
                GivenUser(UserId.NewUserId().Value, "name2", "user2@email.com", "password2"),
                GivenUser(UserId.NewUserId().Value, "name3", "user3@email.com", "password3"),
            };
            _repositoryMock
                .Setup(x => x.SelectAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUsers);
            var sut = new ListUsersQueryHandler(_repositoryMock.Object);

            var actualResult = await sut.Handle(new ListUsersQuery(0, 10), CancellationToken.None);

            actualResult.Should()
                .BeOfType<ListUsersQueryResult>()
                .Which.Users
                .Should().NotBeEmpty()
                .And.Contain(expectedUsers)
                .And.HaveCount(3);
            _repositoryMock.Verify(x => x.SelectAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        
        private static User GivenUser(Guid userId, string name, string email, string password)
        {
            return new(
                new UserId(userId),
                new Name(name),
                new Email(email),
                new Password(password));
        }
    }
}