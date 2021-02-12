using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage;
using RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage.ImageDownloaders;
using RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage.ImageRotators;

namespace RealWorldOne.KittenGenerator.Api.UseCases.GetRandomKittenImage
{
    public static class Dependencies
    {
        public static IServiceCollection AddGetKittenImageUseCase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDownloadImage>(_ => new KittenImageDownloader(new Uri(configuration["Cataas:ApiUrl"])));
            services.AddSingleton<IRotateImage, KittenImageRotator>();
            services.AddSingleton<IKittenImageService, KittenImageService>();

            return services;
        }
    }
}