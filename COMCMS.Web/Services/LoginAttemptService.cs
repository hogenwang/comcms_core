using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace COMCMS.Web.Services
{
    public sealed class LoginAttemptService
    {
        private const int AttemptLimit = 10;
        private static readonly TimeSpan Window = TimeSpan.FromMinutes(15);
        private readonly IConnectionMultiplexer _redis;
        private readonly ConcurrentDictionary<string, AttemptBucket> _local = new();

        public LoginAttemptService(IServiceProvider services)
        {
            _redis = services.GetService<IConnectionMultiplexer>();
        }

        public async Task<bool> IsBlockedAsync(string userName, string ipAddress)
        {
            var accountKey = BuildKey("account", NormalizeUserName(userName));
            var ipKey = BuildKey("ip", ipAddress ?? "unknown");
            if (_redis != null)
            {
                var database = _redis.GetDatabase();
                var values = await Task.WhenAll(database.StringGetAsync(accountKey), database.StringGetAsync(ipKey));
                return Parse(values[0]) >= AttemptLimit || Parse(values[1]) >= AttemptLimit;
            }

            return CurrentLocalCount(accountKey) >= AttemptLimit || CurrentLocalCount(ipKey) >= AttemptLimit;
        }

        public async Task RecordFailureAsync(string userName, string ipAddress)
        {
            var accountKey = BuildKey("account", NormalizeUserName(userName));
            var ipKey = BuildKey("ip", ipAddress ?? "unknown");
            if (_redis != null)
            {
                await Task.WhenAll(IncrementRedisAsync(accountKey), IncrementRedisAsync(ipKey));
                return;
            }

            IncrementLocal(accountKey);
            IncrementLocal(ipKey);
        }

        public async Task RecordSuccessAsync(string userName)
        {
            var accountKey = BuildKey("account", NormalizeUserName(userName));
            if (_redis != null)
            {
                await _redis.GetDatabase().KeyDeleteAsync(accountKey);
                return;
            }
            _local.TryRemove(accountKey, out _);
        }

        private async Task IncrementRedisAsync(string key)
        {
            var database = _redis.GetDatabase();
            const string script = "local count = redis.call('INCR', KEYS[1]); " +
                                  "if count == 1 then redis.call('PEXPIRE', KEYS[1], ARGV[1]); end; " +
                                  "return count;";
            await database.ScriptEvaluateAsync(script, new RedisKey[] { key }, new RedisValue[] { (long)Window.TotalMilliseconds });
        }

        private int CurrentLocalCount(string key)
        {
            if (!_local.TryGetValue(key, out var bucket)) return 0;
            if (bucket.ExpiresUtc > DateTimeOffset.UtcNow) return bucket.Count;
            _local.TryRemove(key, out _);
            return 0;
        }

        private void IncrementLocal(string key)
        {
            var now = DateTimeOffset.UtcNow;
            _local.AddOrUpdate(key,
                _ => new AttemptBucket(1, now.Add(Window)),
                (_, current) => current.ExpiresUtc <= now
                    ? new AttemptBucket(1, now.Add(Window))
                    : new AttemptBucket(current.Count + 1, current.ExpiresUtc));
        }

        private static long Parse(RedisValue value) => value.TryParse(out long count) ? count : 0;
        private static string NormalizeUserName(string value) => (value ?? string.Empty).Trim().ToUpperInvariant();

        private static string BuildKey(string dimension, string value)
        {
            var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
            return $"COMCMS:auth-failure:{dimension}:{hash}";
        }

        private sealed record AttemptBucket(int Count, DateTimeOffset ExpiresUtc);
    }
}
