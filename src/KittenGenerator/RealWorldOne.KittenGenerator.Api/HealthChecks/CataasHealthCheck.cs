using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RealWorldOne.KittenGenerator.Application.UseCases.GetRandomKittenImage;

namespace RealWorldOne.KittenGenerator.Api.HealthChecks
{
    public class CataasHealthCheck : IHealthCheck
    {
        private readonly IDownloadImage _imageDownloader;

        public CataasHealthCheck(IDownloadImage imageDownloader)
        {
            _imageDownloader = imageDownloader;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                _imageDownloader.Download();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"{nameof(CataasHealthCheck)}: Exception during check: {ex.GetType().FullName}", ex);
            }

            return HealthCheckResult.Healthy($"{nameof(CataasHealthCheck)}: Healthy");
        }
    }
}