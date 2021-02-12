using RealWorldOne.UserManagement.Application.Common.Interfaces;

namespace RealWorldOne.UserManagement.Application.UseCases.LoginUser
{
    public class LoginUserCommandResult : ICommandResult
    {
        public LoginUserCommandResult(string accessToken, int expiresIn)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }

        public string AccessToken { get; }

        public int ExpiresIn { get; }
    }
}