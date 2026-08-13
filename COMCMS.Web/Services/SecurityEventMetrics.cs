using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace COMCMS.Web.Services
{
    public sealed class SecurityEventMetrics
    {
        public const string MeterName = "COMCMS.Security";
        private readonly Counter<long> _authenticationAttempts;
        private readonly Counter<long> _rateLimitRejections;
        private readonly Counter<long> _tokenEvents;
        private readonly Counter<long> _sessionRevocations;

        public SecurityEventMetrics(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create(MeterName);
            _authenticationAttempts = meter.CreateCounter<long>("comcms.auth.attempts");
            _rateLimitRejections = meter.CreateCounter<long>("comcms.auth.rate_limit_rejections");
            _tokenEvents = meter.CreateCounter<long>("comcms.auth.token_events");
            _sessionRevocations = meter.CreateCounter<long>("comcms.auth.session_revocations");
        }

        public void AuthenticationAttempt(string subjectType, string scheme, bool success) =>
            _authenticationAttempts.Add(1,
                new KeyValuePair<string, object>("subject.type", subjectType),
                new KeyValuePair<string, object>("auth.scheme", scheme),
                new KeyValuePair<string, object>("result", success ? "success" : "failure"));

        public void RateLimitRejected(string policy) =>
            _rateLimitRejections.Add(1, new KeyValuePair<string, object>("policy", policy));

        public void TokenEvent(string operation, string result) =>
            _tokenEvents.Add(1,
                new KeyValuePair<string, object>("operation", operation),
                new KeyValuePair<string, object>("result", result));

        public void SessionRevoked(string scope) =>
            _sessionRevocations.Add(1, new KeyValuePair<string, object>("scope", scope));
    }
}
