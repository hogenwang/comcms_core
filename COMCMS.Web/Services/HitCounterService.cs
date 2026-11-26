using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using COMCMS.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace COMCMS.Web.Services
{
    /// <summary>
    /// Aggregates public content view hits in memory and flushes them periodically
    /// to avoid synchronous database writes on every GET request.
    /// </summary>
    public sealed class HitCounterService : BackgroundService
    {
        private static readonly TimeSpan FlushInterval = TimeSpan.FromSeconds(30);
        private readonly ConcurrentDictionary<int, int> _articleHits = new();
        private readonly ConcurrentDictionary<int, int> _productHits = new();
        private readonly ConcurrentDictionary<int, int> _tagHits = new();
        private readonly ILogger<HitCounterService> _logger;

        public HitCounterService(ILogger<HitCounterService> logger)
        {
            _logger = logger;
        }

        public void IncrementArticleHit(int articleId)
        {
            if (articleId > 0)
                _articleHits.AddOrUpdate(articleId, 1, (_, count) => count + 1);
        }

        public void IncrementProductHit(int productId)
        {
            if (productId > 0)
                _productHits.AddOrUpdate(productId, 1, (_, count) => count + 1);
        }

        public void IncrementTagHit(int tagId)
        {
            if (tagId > 0)
                _tagHits.AddOrUpdate(tagId, 1, (_, count) => count + 1);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(FlushInterval);
            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    FlushPendingHits();
                }
            }
            catch (OperationCanceledException)
            {
                // Expected during graceful host shutdown.
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            FlushPendingHits();
        }

        public void FlushPendingHits()
        {
            FlushDictionary(_articleHits, (id, delta) =>
            {
                var entity = Article.FindById(id);
                if (entity != null)
                {
                    entity.Hits += delta;
                    entity.Update();
                }
            }, "article");

            FlushDictionary(_productHits, (id, delta) =>
            {
                var entity = Product.FindById(id);
                if (entity != null)
                {
                    entity.Hits += delta;
                    entity.Update();
                }
            }, "product");

            FlushDictionary(_tagHits, (id, delta) =>
            {
                var entity = Tags.FindById(id);
                if (entity != null)
                {
                    entity.Hits += delta;
                    entity.Update();
                }
            }, "tag");
        }

        private void FlushDictionary(ConcurrentDictionary<int, int> bucket, Action<int, int> updater, string entityName)
        {
            foreach (var key in bucket.Keys)
            {
                if (!bucket.TryRemove(key, out var delta) || delta <= 0)
                    continue;
                try
                {
                    updater(key, delta);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to flush {Delta} hit(s) for {EntityName} {Id}.", delta, entityName, key);
                }
            }
        }
    }
}
