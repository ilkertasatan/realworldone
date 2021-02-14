using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Application.UseCases.LoginUser;

namespace RealWorldOne.UserManagement.Api.UseCases.LoginUser
{
    public static class Output
    {
        public static IActionResult For(ICommandResult output) =>
            output switch
            {
                LoginUserCommandResult result => Ok(result),
                UserNotFoundResult result => NotFound(result),
                PasswordInCorrectResult => Unauthorized(),
                _ => InternalServerError()
            };

        private static OkObjectResult Ok(LoginUserCommandResult result)
        {
            return new(new LoginUserResponse
            {
                AccessToken = result.AccessToken,
                ExpiresIn = result.ExpiresIn
            });
        }

        private static NotFoundObjectResult NotFound(UserNotFoundResult notFound)
        {
            return new(notFound.Message);
        }

        private static UnauthorizedObjectResult Unauthorized()
        {
            return new("Password is incorrect");
        }

        private static StatusCodeResult InternalServerError()
        {
            return new(StatusCodes.Status500InternalServerError);
        }
    }
}