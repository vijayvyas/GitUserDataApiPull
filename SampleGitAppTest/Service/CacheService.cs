using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace SampleAppTest
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheConfiguration _cacheConfig;
        private readonly MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions();

        public CacheService(IMemoryCache memoryCache, IConfiguration configuration, IOptions<CacheConfiguration> cacheConfig)
        {
            _memoryCache = memoryCache;
            _cacheConfig = cacheConfig.Value;
            if (_cacheConfig != null)
            {
                cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(_cacheConfig.ExpirationInMinutes));
            }
        }

        T ICacheService.getFromCache<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        void ICacheService.updateCache<T>(string key, T Obj)
        {
            _memoryCache.Set(key, Obj, cacheEntryOptions);
        }
    }
}
