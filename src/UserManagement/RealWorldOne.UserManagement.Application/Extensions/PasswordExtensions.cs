using RealWorldOne.UserManagement.Domain.Users.ValueObjects;
using RealWorldOne.UserManagement.Infrastructure.Security;

namespace RealWorldOne.UserManagement.Application.Extensions
{
    public static class PasswordExtensions
    {
        public static Password ToHash(this Password password)
        {
            return new(Hash.Create(password.Value, Salt.Create()));
        }
    }
}