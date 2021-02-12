using FluentValidation;

namespace RealWorldOne.UserManagement.Application.UseCases.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email.Value)
                .NotNull()
                .WithMessage("The 'email' field cannot be null.")
                .NotEmpty()
                .WithMessage("The 'email' field is required.")
                .EmailAddress()
                .WithMessage("The 'email' field is not a valid e-mail address.");
            
            RuleFor(x => x.Password.Value)
                .NotNull()
                .WithMessage("The 'password' field cannot be null.")
                .NotEmpty()
                .WithMessage("The 'password' field is required.")
                .MaximumLength(30)
                .WithMessage("The 'password' field should be less than 30 characters.");
        }
    }
}