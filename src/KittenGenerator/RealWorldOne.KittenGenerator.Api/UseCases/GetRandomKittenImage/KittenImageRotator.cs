using System.Drawing;
using RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage;

namespace RealWorldOne.KittenGenerator.Api.UseCases.GetRandomKittenImage
{
    public class KittenImageRotator : IRotateImage
    {
        public Image Rotate(Image image)
        {
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            return image;
        }
    }
}