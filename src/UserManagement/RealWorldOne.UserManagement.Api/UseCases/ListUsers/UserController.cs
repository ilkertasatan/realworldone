using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWorldOne.UserManagement.Application.UseCases.ListUsers;

namespace RealWorldOne.UserManagement.Api.UseCases.ListUsers
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
        
        [HttpGet]
        [ProducesResponseType(typeof(ListUsersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListUsersAsync([FromQuery] ListUsersRequest request)
        {
            var queryResult = await _mediator.Send(new ListUsersQuery(request.Offset, request.Limit));
            return Output.For(queryResult);
        }
    }
}