using System;
using System.Threading.Tasks;

namespace COMCMS.Web.Services
{
    /// <summary>
    /// 缓存服务接口
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        Task RemoveAsync(string key);

        /// <summary>
        /// 根据模式移除缓存
        /// </summary>
        /// <param name="pattern">模式</param>
        /// <returns></returns>
        Task RemoveByPatternAsync(string pattern);

        /// <summary>
        /// 检查缓存是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// 获取或设置缓存
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="factory">数据工厂方法</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null);

        /// <summary>
        /// 刷新缓存过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        Task RefreshAsync(string key, TimeSpan expiration);

        /// <summary>
        /// 获取缓存大小
        /// </summary>
        /// <returns></returns>
        Task<long> GetCacheSizeAsync();

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        /// <returns></returns>
        Task ClearAllAsync();
    }
}
