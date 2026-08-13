using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;
using XCode.DataAccessLayer;
using Microsoft.Extensions.Configuration;

namespace COMCMS.Web.Services
{
    public sealed class ComCmsReadinessHealthCheck : IHealthCheck
    {
        private readonly JwtKeyProvider _jwtKeys;
        private readonly IConnectionMultiplexer _redis;
        private readonly bool _hasDatabase;

        public ComCmsReadinessHealthCheck(JwtKeyProvider jwtKeys, IServiceProvider services, IConfiguration configuration)
        {
            _jwtKeys = jwtKeys;
            _redis = services.GetService(typeof(IConnectionMultiplexer)) as IConnectionMultiplexer;
            _hasDatabase = !string.IsNullOrWhiteSpace(configuration["connectionStrings:dbconn:connectionString"]);
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (!_jwtKeys.CanSign)
                return HealthCheckResult.Unhealthy("JWT signing is not available.");
            if (!_hasDatabase)
                return HealthCheckResult.Unhealthy("The database connection is not configured.");

            try
            {
                await DAL.Create("dbconn").ExecuteScalarAsync<int>("SELECT 1");
                if (_redis != null)
                    await _redis.GetDatabase().PingAsync().WaitAsync(cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch (Exception exception)
            {
                return HealthCheckResult.Unhealthy("A required dependency is not available.", exception);
            }
        }
    }
}
