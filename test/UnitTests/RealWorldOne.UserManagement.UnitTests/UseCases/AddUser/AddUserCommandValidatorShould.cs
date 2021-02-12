using FluentValidation.TestHelper;
using RealWorldOne.UserManagement.Application.UseCases.AddUser;
using Xunit;

namespace RealWorldOne.UserManagement.UnitTests.UseCases.AddUser
{
    public class AddUserCommandValidatorShould
    {
        private readonly AddUserCommandValidator _sut;

        public AddUserCommandValidatorShould()
        {
            _sut = new AddUserCommandValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Have_Validation_Error_When_Name_Is_Null_Or_Empty(string name)
        {
            var expectedCommand = new AddUserCommand(name, "user@email.com", "pass");
            
            var actualResult = _sut.TestValidate(expectedCommand);

            actualResult.ShouldHaveValidationErrorFor(user => user.Name.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid-email")]
        public void Have_Validation_Error_When_Email_Is_Null_Or_Empty_Or_Invalid(string email)
        {
            var expectedCommand = new AddUserCommand("name", email, "pass");
            
            var actualResult = _sut.TestValidate(expectedCommand);

            actualResult.ShouldHaveValidationErrorFor(user => user.Email.Value);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Have_Validation_Error_When_Password_Is_Null_Or_Empty(string password)
        {
            var expectedCommand = new AddUserCommand(password, "user@email.com", password);
            
            var actualResult = _sut.TestValidate(expectedCommand);

            actualResult.ShouldHaveValidationErrorFor(user => user.Password.Value);
        }
    }
}