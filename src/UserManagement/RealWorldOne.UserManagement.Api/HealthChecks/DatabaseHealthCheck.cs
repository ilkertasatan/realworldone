using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RealWorldOne.UserManagement.Infrastructure.DataAccess;

namespace RealWorldOne.UserManagement.Api.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly UserManagementDataContext _dataContext;

        public DatabaseHealthCheck(UserManagementDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                var user = _dataContext.Users.Take(1);
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"{nameof(DatabaseHealthCheck)}: Exception during check: {ex.GetType().FullName}", ex);
            }

            return HealthCheckResult.Healthy($"{nameof(DatabaseHealthCheck)}: Healthy");
        }
    }
}