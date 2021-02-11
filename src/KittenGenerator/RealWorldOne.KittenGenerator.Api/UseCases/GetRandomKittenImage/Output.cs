using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage;

namespace RealWorldOne.KittenGenerator.Api.UseCases.GetRandomKittenImage
{
    public static class Output
    {
        public static IActionResult For(IKittenImageResult output) =>
            output switch
            {
                KittenImageSuccessResult success => File(success.ImageBytes),
                KittenImageErrorResult => BadGateway(),
                _ => InternalServerError()
            };

        private static FileContentResult File(byte[] imageBytes)
        {
            return new(imageBytes, "image/jpeg");
        }

        private static StatusCodeResult InternalServerError()
        {
            return new(StatusCodes.Status500InternalServerError);
        }

        private static StatusCodeResult BadGateway()
        {
            return new(StatusCodes.Status502BadGateway);
        }
    }
}