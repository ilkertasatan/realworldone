using FluentValidation;

namespace RealWorldOne.UserManagement.Application.UseCases.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.Name.Value)
                .NotNull()
                .WithMessage("The 'name' field cannot be null.")
                .NotEmpty()
                .WithMessage("The 'name' field is required.")
                .MaximumLength(100)
                .WithMessage("The 'name' field should be less than 100 characters.");

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