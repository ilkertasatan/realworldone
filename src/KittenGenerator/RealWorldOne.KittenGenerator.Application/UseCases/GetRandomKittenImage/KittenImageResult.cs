namespace RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage
{
    public interface IKittenImageResult
    {
    }

    public sealed class KittenImageSuccessResult : IKittenImageResult
    {
        public KittenImageSuccessResult(byte[] imageBytes)
        {
            ImageBytes = imageBytes;
        }

        public byte[] ImageBytes { get; }
    }
    
    public sealed class KittenImageErrorResult : IKittenImageResult
    {
    }
}