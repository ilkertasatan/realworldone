using System.Drawing;

namespace RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage
{
    public interface IRotateImage
    {
        Image Rotate(Image image);
    }
}