using RealWorldOne.UserManagement.Application.Common.Security;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;

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