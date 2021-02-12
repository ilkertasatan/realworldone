using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;

namespace RealWorldOne.UserManagement.Domain
{
    public interface IUserFactory
    {
        User NewUser(UserId userId, Name name, Email email, Password password);
    }
}