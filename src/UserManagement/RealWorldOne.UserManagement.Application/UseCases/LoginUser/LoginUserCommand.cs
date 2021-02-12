using MediatR;
using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;

namespace RealWorldOne.UserManagement.Application.UseCases.LoginUser
{
    public sealed class LoginUserCommand : IRequest<ICommandResult>
    {
        public LoginUserCommand(string email, string password)
        {
            Email = new Email(email);
            Password = new Password(password);
        }

        public Email Email { get; }
        public Password Password { get; }
    }
    
    
}