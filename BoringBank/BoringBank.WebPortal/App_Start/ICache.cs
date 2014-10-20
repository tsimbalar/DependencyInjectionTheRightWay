using System;

namespace BoringBank.WebPortal
{
    public interface ICache
    {
        T GetOrAdd<T>(string cacheKey, Func<T> ifNotInCacheCallBack);
    }
}