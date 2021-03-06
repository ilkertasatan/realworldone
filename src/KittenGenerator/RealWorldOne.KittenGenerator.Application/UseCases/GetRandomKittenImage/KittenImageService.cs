﻿using System;
using RealWorldOne.KittenGenerator.Application.Extensions;

namespace RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage
{
    public class KittenImageService : IKittenImageService
    {
        private readonly IDownloadImage _imageDownloader;
        private readonly IRotateImage _imageRotator;

        public KittenImageService(
            IDownloadImage imageDownloader,
            IRotateImage imageRotator)
        {
            _imageDownloader = imageDownloader;
            _imageRotator = imageRotator;
        }

        public IKittenImageResult GetRandomImage()
        {
            try
            {
                var rotatedImage = _imageRotator.Rotate(_imageDownloader.Download());
                return new KittenImageSuccessResult(rotatedImage.ToBytes());
            }
            catch (Exception)
            {
                return new KittenImageErrorResult();
            }
        }
    }
}