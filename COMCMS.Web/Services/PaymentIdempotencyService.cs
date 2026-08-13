using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace COMCMS.Web.Services
{
    public sealed class PaymentIdempotencyService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly ConcurrentDictionary<string, DateTimeOffset> _localLocks = new();

        public PaymentIdempotencyService(IServiceProvider services)
        {
            _redis = services.GetService<IConnectionMultiplexer>();
        }

        public async Task<bool> TryAcquireAsync(int memberId, string orderNumber, string idempotencyKey, TimeSpan lifetime)
        {
            var key = BuildKey(memberId, orderNumber, idempotencyKey);
            if (_redis != null)
            {
                return await _redis.GetDatabase().StringSetAsync(key, "processing", lifetime, When.NotExists);
            }

            var now = DateTimeOffset.UtcNow;
            foreach (var item in _localLocks)
            {
                if (item.Value <= now) _localLocks.TryRemove(item.Key, out _);
            }
            return _localLocks.TryAdd(key, now.Add(lifetime));
        }

        public async Task ReleaseAsync(int memberId, string orderNumber, string idempotencyKey)
        {
            var key = BuildKey(memberId, orderNumber, idempotencyKey);
            if (_redis != null)
            {
                await _redis.GetDatabase().KeyDeleteAsync(key);
                return;
            }
            _localLocks.TryRemove(key, out _);
        }

        private static string BuildKey(int memberId, string orderNumber, string idempotencyKey)
        {
            var value = $"{memberId}\n{orderNumber}\n{idempotencyKey}";
            var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
            return $"COMCMS:payment:idempotency:{hash}";
        }
    }
}
