using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RealWorldOne.UserManagement.Api.UseCases.AddUser;
using Xunit;

namespace RealWorldOne.UserManagement.UnitTests.UseCases.AddUser
{
    public class UserControllerShould
    {
        [Fact]
        public async Task Return_201_When_User_Registered()
        {
            var sut = new UserController();

            var actualResult = await sut.AddNewUser();

            actualResult.Should().BeOfType<CreatedResult>();
        }
    }
}