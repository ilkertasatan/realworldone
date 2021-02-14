using FluentAssertions;
using RealWorldOne.UserManagement.Infrastructure.Security;
using Xunit;

namespace RealWorldOne.KittenGenerator.IntegrationTests.SecurityTests
{
    public class PasswordHasherShould
    {
        [Fact]
        public void Verify_Password_When_Hash_Matches_Password()
        {
            const string password = "passw0rd";
            var expectedSalt = Salt.Create();
            var expectedHash = Hash.Create(password, expectedSalt);

            var actualResult = Hash.Verify(password, expectedSalt, expectedHash);

            actualResult.Should().BeTrue();
        }
        
        [Fact]
        public void Not_Verify_Password_When_Hash_Does_Not_Match_Password()
        {
            const string password = "passw0rd";
            var expectedSalt = Salt.Create();
            const string expectedHash = "unmatched_hash";

            var actualResult = Hash.Verify(password, expectedSalt, expectedHash);

            actualResult.Should().BeFalse();
        }
    }
}