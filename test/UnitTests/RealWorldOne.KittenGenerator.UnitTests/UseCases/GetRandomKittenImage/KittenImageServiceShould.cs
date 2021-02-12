using System.Drawing;
using FluentAssertions;
using Moq;
using RealWorldOne.KittenGenerator.Application.Extensions;
using RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage;
using Xunit;

namespace RealWorldOne.KittenGenerator.UnitTests.UseCases.GetRandomKittenImage
{
    public class KittenImageServiceShould
    {
        private readonly Mock<IDownloadImage> _imageDownloaderMock;
        private readonly Mock<IRotateImage> _imageRotatorMock;
        private readonly IKittenImageService _sut;

        public KittenImageServiceShould()
        {
            _imageDownloaderMock = new Mock<IDownloadImage>();
            _imageRotatorMock = new Mock<IRotateImage>();
            _sut = new KittenImageService(_imageDownloaderMock.Object, _imageRotatorMock.Object);
        }

        [Fact]
        public void Return_Kitten_Image_Success_Result_When_Kitten_Image_Downloaded_Correctly()
        {
            var expectedImage = Image.FromFile("UseCases\\GetRandomKittenImage\\cat.jfif");
            var expectedRotatedImage = Image.FromFile("UseCases\\GetRandomKittenImage\\cat_rotated.jfif");
            var expectedImageBytes = expectedRotatedImage.ToBytes();
            _imageDownloaderMock.Setup(x => x.Download())
                .Returns(expectedImage);
            _imageRotatorMock
                .Setup(x => x.Rotate(expectedImage))
                .Returns(expectedRotatedImage);

            var actualResult = _sut.GetRandomImage();

            actualResult.Should()
                .BeOfType<KittenImageSuccessResult>()
                .Which.ImageBytes
                .Should().BeEquivalentTo(expectedImageBytes);
        }

        [Fact]
        public void Return_Kitten_Image_Error_Image_Result_When_Error_Occured()
        {
            _imageDownloaderMock.Setup(x => x.Download())
                .Returns(It.IsAny<Image>());
            _imageRotatorMock
                .Setup(x => x.Rotate(It.IsAny<Image>()))
                .Returns(It.IsAny<Image>());

            var actualResult = _sut.GetRandomImage();

            actualResult.Should().BeOfType<KittenImageErrorResult>();
        }
    }
}