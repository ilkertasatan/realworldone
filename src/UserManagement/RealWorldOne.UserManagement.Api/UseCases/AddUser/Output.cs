using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Application.UseCases.AddUser;
using RealWorldOne.UserManagement.Domain.Users;

namespace RealWorldOne.UserManagement.Api.UseCases.AddUser
{
    public static class Output
    {
        public static IActionResult For(ICommandResult output) =>
            output switch
            {
                AddUserCommandResult result => Created(result.User),
                _ => InternalServerError()
            };
        
        private static CreatedResult Created(User user)
        {
            return new("api/users", new AddUserResponse
            {
                UserId = user.Id.Value,
                Name = user.Name.Value,
                Email = user.Email.Value
            });
        }

        private static StatusCodeResult InternalServerError()
        {
            return new(StatusCodes.Status500InternalServerError);
        }
    }
}