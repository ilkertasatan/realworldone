using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealWorldOne.UserManagement.Api.UseCases.ListUsers;
using RealWorldOne.UserManagement.Application.UseCases.ListUsers;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;
using Xunit;

namespace RealWorldOne.UserManagement.UnitTests.UseCases.ListUsers
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
        public async Task Return_200_With_Users()
        {
            const string expectedName1 = "name-1";
            const string expectedEmail1 = "user1@email.com";
            const string expectedPassword1 = "password1";
            const string expectedName2 = "name-2";
            const string expectedEmail2 = "user2@email.com";
            const string expectedPassword2 = "password2";
            const string expectedName3 = "name-3";
            const string expectedEmail3 = "user3@email.com";
            const string expectedPassword3 = "password3";
            var expectedUserId1 = UserId.NewUserId().Value;
            var expectedUserId2 = UserId.NewUserId().Value;
            var expectedUserId3 = UserId.NewUserId().Value;
            var expectedResponse = new List<ListUsersResponse>
            {
                new() {Id = expectedUserId1, Name = expectedName1, Email = expectedEmail1},
                new() {Id = expectedUserId2, Name = expectedName2, Email = expectedEmail2},
                new() {Id = expectedUserId3, Name = expectedName3, Email = expectedEmail3},
            };
            var expectedUser = new List<User>
            {
                GivenUser(expectedUserId1, expectedName1, expectedEmail1, expectedPassword1),
                GivenUser(expectedUserId2, expectedName2, expectedEmail2, expectedPassword2),
                GivenUser(expectedUserId3, expectedName3, expectedEmail3, expectedPassword3)
            };
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<ListUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ListUsersQueryResult(expectedUser));

            var actualResult = await _sut.ListUsersAsync(new ListUsersRequest
            {
                Offset = 0,
                Limit = 10
            });

            actualResult.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<List<ListUsersResponse>>()
                .Which.Should().NotBeEmpty()
                .And.Contain(expectedResponse)
                .And.HaveCount(3);
            _mediatorMock.Verify(x => x.Send(It.IsAny<ListUsersQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Return_200_With_Empty_List_When_No_Users()
        {
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<ListUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ListUsersQueryResult(new List<User>()));

            var actualResult = await _sut.ListUsersAsync(new ListUsersRequest
            {
                Offset = 0,
                Limit = 10
            });

            actualResult.Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<List<ListUsersResponse>>()
                .Which.Should().BeEmpty()
                .And.HaveCount(0);
            _mediatorMock.Verify(x => x.Send(It.IsAny<ListUsersQuery>(), It.IsAny<CancellationToken>()), Times.Once); 
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