using RealWorldOne.UserManagement.Application.Common.Security;

namespace RealWorldOne.UserManagement.Application.Common.Interfaces
{
    public interface IGenerateAccessToken
    {
        AccessToken CreateAccessToken(string secret, string sub, string iss, string aud);
    }
}