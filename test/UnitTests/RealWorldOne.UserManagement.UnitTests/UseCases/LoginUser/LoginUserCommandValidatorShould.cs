using FluentValidation.TestHelper;
using RealWorldOne.UserManagement.Application.UseCases.LoginUser;
using Xunit;

namespace RealWorldOne.UserManagement.UnitTests.UseCases.LoginUser
{
    public class LoginUserCommandValidatorShould
    {
        private readonly LoginUserCommandValidator _sut;

        public LoginUserCommandValidatorShould()
        {
            _sut = new LoginUserCommandValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid-email")]
        public void Have_Validation_Error_When_Email_Is_Null_Or_Empty_Or_Invalid(string email)
        {
            var expectedCommand = new LoginUserCommand(email, "pass");
            
            var actualResult = _sut.TestValidate(expectedCommand);

            actualResult.ShouldHaveValidationErrorFor(user => user.Email.Value);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Have_Validation_Error_When_Password_Is_Null_Or_Empty(string password)
        {
            var expectedCommand = new LoginUserCommand("user@email.com", password);
            
            var actualResult = _sut.TestValidate(expectedCommand);

            actualResult.ShouldHaveValidationErrorFor(user => user.Password.Value);
        }
    }
}