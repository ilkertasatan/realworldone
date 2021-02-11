using System.Net;
using Microsoft.AspNetCore.Mvc;
using RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage;

namespace RealWorldOne.KittenGenerator.Api.UseCases.GetRandomKittenImage
{
    [Route("api/kittens")]
    [ApiController]
    public class KittenImageController : ControllerBase
    {
        private readonly IKittenImageService _kittenImageService;

        public KittenImageController(IKittenImageService kittenImageService)
        {
            _kittenImageService = kittenImageService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetRandomKittenImage()
        {
            var result = _kittenImageService.GetRandomImage();
            return Output.For(result);
        }
    }
}