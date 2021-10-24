using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace SelfWealthTest
{
    public class CacheService<T> : ICacheService<T> where T:class
    {
        readonly IMemoryCache _memoryCache;
        MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
       .SetSlidingExpiration(TimeSpan.FromMinutes(2));

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        T ICacheService<T>.getFromCache(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        void ICacheService<T>.updateCache(string key, T Obj)
        {
            _memoryCache.Set(key, Obj, cacheEntryOptions);
        }
    }
}
