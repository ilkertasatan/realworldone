using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RealWorldOne.KittenGenerator.Api.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        {
            // services.AddHealthChecks().AddCheck<LivenessHealthCheck>("Liveness", HealthStatus.Unhealthy);
            // services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("Readiness", HealthStatus.Unhealthy);
            //
            return services;
        }
    }
}