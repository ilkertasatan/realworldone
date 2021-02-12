using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Application.UseCases.ListUsers;
using RealWorldOne.UserManagement.Domain.Users;

namespace RealWorldOne.UserManagement.Api.UseCases.ListUsers
{
    public static class Output
    {
        public static IActionResult For(IQueryResult output) =>
            output switch
            {
                ListUsersQueryResult result => Ok(result.Users),
                _ => InternalServerError()
            };
        
        private static OkObjectResult Ok(IEnumerable<User> users)
        {
            return new(users.Select(c => new ListUsersResponse()
            {
                Id = c.Id.Value,
                Name = c.Name.Value,
                Email = c.Email.Value
            }).ToList());
        }

        private static StatusCodeResult InternalServerError()
        {
            return new(StatusCodes.Status500InternalServerError);
        }
    }
}