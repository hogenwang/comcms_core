using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using COMCMS.Common.Security;
using COMCMS.Core;
using Microsoft.Extensions.Caching.Memory;

namespace COMCMS.Web.Services
{
    /// <summary>
    /// Short-lived validation cache for Admin cookie and Member session checks to avoid
    /// hitting the database on every authenticated request while supporting immediate invalidation.
    /// </summary>
    public sealed class AuthValidationCache
    {
        private static readonly TimeSpan CacheTtl = TimeSpan.FromSeconds(15);
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<int, ConcurrentDictionary<string, byte>> _adminKeys = new();
        private readonly ConcurrentDictionary<int, ConcurrentDictionary<string, byte>> _memberKeys = new();

        public AuthValidationCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool ValidateAdmin(ClaimsPrincipal principal)
        {
            if (principal == null) return false;
            var adminId = ComCmsClaimTypes.GetSubjectId(principal);
            var actualStamp = principal.FindFirstValue(ComCmsClaimTypes.SecurityStamp);
            if (adminId <= 0 || string.IsNullOrWhiteSpace(actualStamp))
                return false;

            var cacheKey = $"auth:admin:{adminId}:{actualStamp}";
            if (_cache.TryGetValue(cacheKey, out bool cached) && cached)
                return true;

            var admin = Admin.Find(Admin._.Id == adminId);
            var expectedStamp = admin == null
                ? null
                : SecurityStampService.Compute("admin", admin.Id, admin.PassWord, admin.RoleId, admin.IsLock);

            if (admin == null || admin.IsLock == 1 || !SecurityStampService.Equals(expectedStamp, actualStamp))
                return false;

            _cache.Set(cacheKey, true, CacheTtl);
            TrackKey(_adminKeys, adminId, cacheKey);
            return true;
        }

        public bool ValidateMemberSession(ClaimsPrincipal principal, TokenService tokenService)
        {
            if (principal == null || tokenService == null) return false;
            var subjectId = ComCmsClaimTypes.GetSubjectId(principal);
            var sessionId = principal.FindFirstValue(ComCmsClaimTypes.SessionId);
            var actualStamp = principal.FindFirstValue(ComCmsClaimTypes.SecurityStamp);
            if (subjectId <= 0 || string.IsNullOrWhiteSpace(sessionId) || string.IsNullOrWhiteSpace(actualStamp))
                return false;

            var cacheKey = $"auth:member:{subjectId}:{sessionId}:{actualStamp}";
            if (_cache.TryGetValue(cacheKey, out bool cached) && cached)
                return true;

            if (!tokenService.ValidateActiveSession(principal))
                return false;

            _cache.Set(cacheKey, true, CacheTtl);
            TrackKey(_memberKeys, subjectId, cacheKey);
            return true;
        }

        public void InvalidateAdmin(int adminId)
        {
            if (adminId <= 0) return;
            if (_adminKeys.TryRemove(adminId, out var keys))
            {
                foreach (var key in keys.Keys)
                    _cache.Remove(key);
            }
        }

        public void InvalidateMember(int memberId)
        {
            if (memberId <= 0) return;
            if (_memberKeys.TryRemove(memberId, out var keys))
            {
                foreach (var key in keys.Keys)
                    _cache.Remove(key);
            }
        }

        private static void TrackKey(ConcurrentDictionary<int, ConcurrentDictionary<string, byte>> map, int id, string key)
        {
            var set = map.GetOrAdd(id, _ => new ConcurrentDictionary<string, byte>());
            if (set.Count > 32)
                set.Clear();
            set[key] = 0;
        }
    }
}
