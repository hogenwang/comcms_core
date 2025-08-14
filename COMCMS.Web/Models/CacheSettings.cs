using System;

namespace COMCMS.Web.Models
{
    /// <summary>
    /// 缓存配置设置
    /// </summary>
    public class CacheSettings
    {
        /// <summary>
        /// 默认过期时间（分钟）
        /// </summary>
        public int DefaultExpirationMinutes { get; set; } = 30;

        /// <summary>
        /// 系统配置缓存过期时间（分钟）
        /// </summary>
        public int SystemConfigExpirationMinutes { get; set; } = 60;

        /// <summary>
        /// 文章缓存过期时间（分钟）
        /// </summary>
        public int ArticleExpirationMinutes { get; set; } = 20;

        /// <summary>
        /// 产品缓存过期时间（分钟）
        /// </summary>
        public int ProductExpirationMinutes { get; set; } = 20;

        /// <summary>
        /// 分类缓存过期时间（分钟）
        /// </summary>
        public int CategoryExpirationMinutes { get; set; } = 120;

        /// <summary>
        /// 用户缓存过期时间（分钟）
        /// </summary>
        public int UserExpirationMinutes { get; set; } = 15;

        /// <summary>
        /// 订单缓存过期时间（分钟）
        /// </summary>
        public int OrderExpirationMinutes { get; set; } = 10;

        /// <summary>
        /// 是否启用响应缓存
        /// </summary>
        public bool EnableResponseCaching { get; set; } = true;

        /// <summary>
        /// 是否启用分布式缓存
        /// </summary>
        public bool EnableDistributedCaching { get; set; } = true;

        /// <summary>
        /// 获取默认过期时间跨度
        /// </summary>
        public TimeSpan DefaultExpiration => TimeSpan.FromMinutes(DefaultExpirationMinutes);

        /// <summary>
        /// 获取系统配置过期时间跨度
        /// </summary>
        public TimeSpan SystemConfigExpiration => TimeSpan.FromMinutes(SystemConfigExpirationMinutes);

        /// <summary>
        /// 获取文章过期时间跨度
        /// </summary>
        public TimeSpan ArticleExpiration => TimeSpan.FromMinutes(ArticleExpirationMinutes);

        /// <summary>
        /// 获取产品过期时间跨度
        /// </summary>
        public TimeSpan ProductExpiration => TimeSpan.FromMinutes(ProductExpirationMinutes);

        /// <summary>
        /// 获取分类过期时间跨度
        /// </summary>
        public TimeSpan CategoryExpiration => TimeSpan.FromMinutes(CategoryExpirationMinutes);

        /// <summary>
        /// 获取用户过期时间跨度
        /// </summary>
        public TimeSpan UserExpiration => TimeSpan.FromMinutes(UserExpirationMinutes);

        /// <summary>
        /// 获取订单过期时间跨度
        /// </summary>
        public TimeSpan OrderExpiration => TimeSpan.FromMinutes(OrderExpirationMinutes);
    }

    /// <summary>
    /// Redis缓存配置
    /// </summary>
    public class RedisCacheSettings
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; } = "server=127.0.0.1;password=;db=2";

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; } = "COMCMS:";

        /// <summary>
        /// 默认数据库
        /// </summary>
        public int DefaultDatabase { get; set; } = 2;

        /// <summary>
        /// 连接超时时间（毫秒）
        /// </summary>
        public int ConnectTimeout { get; set; } = 5000;

        /// <summary>
        /// 同步超时时间（毫秒）
        /// </summary>
        public int SyncTimeout { get; set; } = 5000;

        /// <summary>
        /// 是否中断连接
        /// </summary>
        public bool AbortConnect { get; set; } = false;

        /// <summary>
        /// 连接重试次数
        /// </summary>
        public int ConnectRetry { get; set; } = 3;
    }
}
