using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RealWorldOne.UserManagement.Api.UseCases.AddUser
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public async Task<IActionResult> AddNewUser()
        {
            throw new System.NotImplementedException();
        }
    }
}