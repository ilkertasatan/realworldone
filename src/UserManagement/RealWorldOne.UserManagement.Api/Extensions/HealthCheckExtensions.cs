using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RealWorldOne.UserManagement.Api.HealthChecks;

namespace RealWorldOne.UserManagement.Api.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck<LivenessHealthCheck>("Liveness", HealthStatus.Unhealthy);
            services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("Readiness", HealthStatus.Unhealthy);
            
            return services;
        }
    }
}