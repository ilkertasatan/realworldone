﻿using System;
using FluentAssertions;
using RealWorldOne.KittenGenerator.Api.UseCases.GetRandomKittenImage;
using Xunit;

namespace RealWorldOne.KittenGenerator.IntegrationTests.ImageDownloaderTests
{
    public class KittenImageDownloaderShould
    {
        [Fact]
        public void Download_Image()
        {
            var sut = new KittenImageDownloader(new Uri("https://cataas.com/cat"));

            var actualResult = sut.Download();

            actualResult.Should().NotBeNull();
            actualResult.Width.Should().Be(600);
            actualResult.Height.Should().Be(450);
        }
    }
}