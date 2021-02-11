using System;
using System.Drawing.Imaging;
using System.IO;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealWorldOne.KittenGenerator.Api.UseCases.GetRandomKittenImage;
using RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage;
using Xunit;

namespace RealWorldOne.KittenGenerator.UnitTests.UseCases.GetRandomKittenImage
{
    public class KittenControllerShould
    {
        private readonly KittenImageController _sut;
        private readonly Mock<IKittenImageService> _kittenImageServiceMock;

        public KittenControllerShould()
        {
            _kittenImageServiceMock = new Mock<IKittenImageService>();
            _sut = new KittenImageController(_kittenImageServiceMock.Object);
        }

        [Fact]
        public void Return_200_When_Kitten_Image_Downloaded_Correctly()
        {
            var expectedImageBytes = GivenRandomByteArray(1024);
            _kittenImageServiceMock
                .Setup(x => x.GetRandomImage())
                .Returns(new KittenImageSuccessResult(expectedImageBytes));
            
            var actualResult = _sut.GetRandomKittenImage();

            actualResult.Should()
                .BeOfType<FileContentResult>()
                .Which.FileContents
                .Should()
                .BeEquivalentTo(expectedImageBytes);
            _kittenImageServiceMock.Verify(x => x.GetRandomImage(), Times.Once);
        }

        [Fact]
        public void Return_502_When_Proxy_Server_Not_Reachable()
        {
            _kittenImageServiceMock
                .Setup(x => x.GetRandomImage())
                .Returns(new KittenImageErrorResult());
            
            var actualResult = _sut.GetRandomKittenImage();

            actualResult.Should()
                .BeOfType<StatusCodeResult>()
                .Which.StatusCode
                .Should()
                .Be(StatusCodes.Status502BadGateway);
            _kittenImageServiceMock.Verify(x => x.GetRandomImage(), Times.Once);
        }

        private static byte[] GivenRandomByteArray(int sizeInKb)
        {
            var random = new Random();
            var bytes = new byte[sizeInKb * 1024];
            random.NextBytes(bytes);

            return bytes;
        }
    }
}