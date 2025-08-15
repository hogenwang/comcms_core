using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMCMS.Common;
using COMCMS.Web.Models;
using Microsoft.Extensions.Options;
using NewLife.Caching;
using NewLife.Caching.Services;

namespace COMCMS.Web.Services
{
	public class CacheService : ICacheService
	{
		private readonly NewLife.Caching.ICacheProvider _cacheProvider;
		private readonly CacheSettings _settings;
		private readonly string _prefix;

		public CacheService(NewLife.Caching.ICacheProvider cacheProvider, IOptions<CacheSettings> settings)
		{
			_cacheProvider = cacheProvider;
			_settings = settings.Value;
			_prefix = Utils.PrefixKey ?? "comcms:";
		}

		private string BuildKey(string key) => string.Concat(_prefix, key ?? string.Empty);

		public async Task<T> GetAsync<T>(string key)
		{
			return await Task.FromResult(_cacheProvider.Cache.Get<T>(BuildKey(key)));
		}

		public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
		{
			var expireSeconds = (int)(expiration ?? _settings.DefaultExpiration).TotalSeconds;
			_cacheProvider.Cache.Add(BuildKey(key), value, expireSeconds);
			await Task.CompletedTask;
		}

		public async Task RemoveAsync(string key)
		{
			_cacheProvider.Cache.Remove(BuildKey(key));
			await Task.CompletedTask;
		}

		public async Task RemoveByPatternAsync(string pattern)
		{
			// 暂不支持模式删除，保留占位实现
			await Task.CompletedTask;
		}

		public async Task<bool> ExistsAsync(string key)
		{
			var val = _cacheProvider.Cache.Get<object>(BuildKey(key));
			return await Task.FromResult(val != null);
		}

		public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
		{
			var cacheKey = BuildKey(key);
			var existed = _cacheProvider.Cache.Get<T>(cacheKey);
			if (!EqualityComparer<T>.Default.Equals(existed, default)) return existed;

			var data = await factory();
			await SetAsync(key, data, expiration);
			return data;
		}

		public async Task RefreshAsync(string key, TimeSpan expiration)
		{
			var cacheKey = BuildKey(key);
			var val = _cacheProvider.Cache.Get<object>(cacheKey);
			if (val != null) _cacheProvider.Cache.Add(cacheKey, val, (int)expiration.TotalSeconds);
			await Task.CompletedTask;
		}

		public async Task<long> GetCacheSizeAsync()
		{
			// 暂无法获取容量，返回0
			return await Task.FromResult(0L);
		}

		public async Task ClearAllAsync()
		{
			// 暂不提供全清功能
			await Task.CompletedTask;
		}
	}
}
