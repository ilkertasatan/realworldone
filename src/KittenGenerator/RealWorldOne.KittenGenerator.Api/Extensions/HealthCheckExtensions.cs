using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RealWorldOne.KittenGenerator.Api.HealthChecks;

namespace RealWorldOne.KittenGenerator.Api.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck<LivenessHealthCheck>("Liveness", HealthStatus.Unhealthy);
            services.AddHealthChecks().AddCheck<CataasHealthCheck>("Readiness", HealthStatus.Unhealthy);
            
            return services;
        }
    }
}