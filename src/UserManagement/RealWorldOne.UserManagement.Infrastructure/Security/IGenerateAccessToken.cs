namespace RealWorldOne.UserManagement.Infrastructure.Security
{
    public interface IGenerateAccessToken
    {
        AccessToken CreateAccessToken(string secret, string sub, string iss, string aud);
    }
}