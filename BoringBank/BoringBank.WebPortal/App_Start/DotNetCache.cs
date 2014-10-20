using System;
using System.Web.Caching;

namespace BoringBank.WebPortal
{
    public class DotNetCache : ICache
    {
        private readonly TimeSpan _cacheDuration;

        public DotNetCache(TimeSpan cacheDuration)
        {
            _cacheDuration = cacheDuration;
        }

        public T GetOrAdd<T>(string cacheKey, Func<T> ifNotInCacheCallBack)
        {
            var cache = System.Web.HttpContext.Current.Cache;

            var cached = cache.Get(cacheKey);
            if (cached == null)
            {
                var newValue = ifNotInCacheCallBack();
                cache.Add(cacheKey, newValue, null, Cache.NoAbsoluteExpiration,
                    _cacheDuration, CacheItemPriority.Default, null);
                cached = newValue;
            }

            return (T)cached;
        }
    }
}