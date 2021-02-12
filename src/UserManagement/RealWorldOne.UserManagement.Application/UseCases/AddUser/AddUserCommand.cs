using MediatR;
using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;

namespace RealWorldOne.UserManagement.Application.UseCases.AddUser
{
    public sealed class AddUserCommand : IRequest<ICommandResult>
    {
        public AddUserCommand(string name, string email, string password)
        {
            Name = new Name(name);
            Email = new Email(email);
            Password = new Password(password);
        }

        public Name Name { get; }
        public Email Email { get; }
        public Password Password { get; }
    }
}