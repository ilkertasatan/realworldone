using System.Drawing;

namespace RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage.ImageRotators
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