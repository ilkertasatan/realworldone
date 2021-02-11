using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RealWorldOne.KittenGenerator.Api.Controllers
{
    [Route("api/recipes")]
    [Produces("application/json")]
    [ApiController]
    public class KittenController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> List()
        {
            return Ok();
        }

    }
}