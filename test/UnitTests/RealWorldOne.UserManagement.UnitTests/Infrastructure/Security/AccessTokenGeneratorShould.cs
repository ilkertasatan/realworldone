using FluentAssertions;
using RealWorldOne.UserManagement.Infrastructure.Security;
using Xunit;

namespace RealWorldOne.UserManagement.UnitTests.Infrastructure.Security
{
    public class AccessTokenGeneratorShould
    {
        [Fact]
        public void Generate_Token()
        {
            const int expectedExpiresIn = 300000;
            var sut = new AccessTokenGenerator();

            var actualResult = sut.CreateAccessToken("aGVsbG9mcmVzaGdvX2JlX3Rlc3Q=", "sub", "iss", "aud");

            actualResult.Token.Should().NotBeNull();
            actualResult.ExpiresIn.Should().Be(expectedExpiresIn);
        }
    }
}