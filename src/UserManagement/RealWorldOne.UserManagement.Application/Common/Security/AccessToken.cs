namespace RealWorldOne.UserManagement.Application.Common.Security
{
    public sealed class AccessToken
    {
        public AccessToken(string accessToken, int expiresIn)
        {
            Token = accessToken;
            ExpiresIn = expiresIn;
        }
        
        public string Token { get; }
        public int ExpiresIn { get; }
    }
}