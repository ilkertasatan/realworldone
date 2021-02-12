using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWorldOne.UserManagement.Application.UseCases.AddUser;

namespace RealWorldOne.UserManagement.Api.UseCases.AddUser
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewUser([FromBody]AddUserRequest request)
        {
            var result = await _mediator.Send(new AddUserCommand(request.Name, request.Email, request.Password));
            return Output.For(result);
        }
    }
}