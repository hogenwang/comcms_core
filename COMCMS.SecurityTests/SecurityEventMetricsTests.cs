using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using COMCMS.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class SecurityEventMetricsTests
    {
        [Fact]
        public void AuthenticationMetrics_ExposeOnlyBoundedTags()
        {
            using var services = new ServiceCollection().AddMetrics().BuildServiceProvider();
            var measurements = new List<KeyValuePair<string, object>[]>();
            using var listener = new MeterListener();
            listener.InstrumentPublished = (instrument, current) =>
            {
                if (instrument.Meter.Name == SecurityEventMetrics.MeterName)
                    current.EnableMeasurementEvents(instrument);
            };
            listener.SetMeasurementEventCallback<long>((_, _, tags, _) => measurements.Add(tags.ToArray()));
            listener.Start();

            var metrics = new SecurityEventMetrics(services.GetRequiredService<IMeterFactory>());
            metrics.AuthenticationAttempt("member", "bearer", false);

            var tags = Assert.Single(measurements);
            Assert.Equal(new[] { "auth.scheme", "result", "subject.type" }, tags.Select(item => item.Key).OrderBy(item => item));
            Assert.DoesNotContain(tags, item => item.Key.Contains("user", StringComparison.OrdinalIgnoreCase) ||
                                                 item.Key.Contains("ip", StringComparison.OrdinalIgnoreCase));
        }
    }
}
