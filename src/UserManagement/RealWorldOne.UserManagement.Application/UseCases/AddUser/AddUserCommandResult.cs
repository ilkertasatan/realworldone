using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Domain.Users;

namespace RealWorldOne.UserManagement.Application.UseCases.AddUser
{
    public class AddUserCommandResult : ICommandResult
    {
        public AddUserCommandResult(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}