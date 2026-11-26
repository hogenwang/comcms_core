using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Web.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace COMCMS.Web.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _settings;
        private readonly string _prefix;

        public CacheService(IDistributedCache cache, IOptions<CacheSettings> settings)
        {
            _cache = cache;
            _settings = settings.Value;
            _prefix = Utils.PrefixKey ?? "comcms:";
        }

        private string BuildKey(string key) => string.Concat(_prefix, key ?? string.Empty);

        public async Task<T> GetAsync<T>(string key)
        {
            var payload = await _cache.GetStringAsync(BuildKey(key));
            if (string.IsNullOrEmpty(payload))
                return default;
            return JsonSerializer.Deserialize<T>(payload);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? _settings.DefaultExpiration
            };
            var payload = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(BuildKey(key), payload, options);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(BuildKey(key));
        }

        public Task RemoveByPatternAsync(string pattern)
        {
            // IDistributedCache does not support pattern deletion natively.
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(string key)
        {
            var payload = await _cache.GetStringAsync(BuildKey(key));
            return !string.IsNullOrEmpty(payload);
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
        {
            var existed = await GetAsync<T>(key);
            if (!EqualityComparer<T>.Default.Equals(existed, default))
                return existed;

            var data = await factory();
            await SetAsync(key, data, expiration);
            return data;
        }

        public async Task RefreshAsync(string key, TimeSpan expiration)
        {
            var fullKey = BuildKey(key);
            var payload = await _cache.GetStringAsync(fullKey);
            if (!string.IsNullOrEmpty(payload))
            {
                await _cache.SetStringAsync(fullKey, payload, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                });
            }
        }

        public Task<long> GetCacheSizeAsync() => Task.FromResult(0L);

        public Task ClearAllAsync() => Task.CompletedTask;
    }
}
