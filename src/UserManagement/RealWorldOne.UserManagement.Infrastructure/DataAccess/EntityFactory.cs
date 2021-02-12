using RealWorldOne.UserManagement.Domain;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;

namespace RealWorldOne.UserManagement.Infrastructure.DataAccess
{
    public sealed class EntityFactory : IUserFactory
    {
        public User NewUser(Name name, Email email, Password password)
        {
            return new(UserId.NewUserId(), name, email, password);
        }
    }
}