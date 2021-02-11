using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealWorldOne.KittenGenerator.Api.UseCases.GetRandomKittenImage;

namespace RealWorldOne.KittenGenerator.Api.Extensions
{
    public static class UseCaseExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGetKittenImageUseCase(configuration);
            return services;
        }
    }
}