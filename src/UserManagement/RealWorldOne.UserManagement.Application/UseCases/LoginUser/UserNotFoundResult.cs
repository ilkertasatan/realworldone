using RealWorldOne.UserManagement.Application.Common.Interfaces;

namespace RealWorldOne.UserManagement.Application.UseCases.LoginUser
{
    public class UserNotFoundResult : ICommandResult
    {
        public UserNotFoundResult(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}