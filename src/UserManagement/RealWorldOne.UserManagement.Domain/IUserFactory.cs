using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;

namespace RealWorldOne.UserManagement.Domain
{
    public interface IUserFactory
    {
        User NewUser(Name name, Email email, Password password, PasswordSalt passwordSalt);
    }
}