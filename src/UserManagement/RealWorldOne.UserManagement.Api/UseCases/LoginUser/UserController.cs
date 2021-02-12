using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWorldOne.UserManagement.Api.UseCases.AddUser;
using RealWorldOne.UserManagement.Application.UseCases.LoginUser;

namespace RealWorldOne.UserManagement.Api.UseCases.LoginUser
{
    [Route("api/users/login")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUserAsync([FromBody]LoginUserRequest request)
        {
            var result = await _mediator.Send(new LoginUserCommand(request.Email, request.Password));
            return Output.For(result);
        }
    }
}