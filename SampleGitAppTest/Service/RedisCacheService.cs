using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Text.Json;

namespace SampleAppTest
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase db;
        private readonly TimeSpan expiration;
        private readonly CacheConfiguration _cacheConfig;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer, IConfiguration configuration, IOptions<CacheConfiguration> cacheConfig)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _cacheConfig = cacheConfig.Value;
            db = _connectionMultiplexer.GetDatabase();
            if (_cacheConfig != null)
            {
                expiration = TimeSpan.FromMinutes(_cacheConfig.ExpirationInMinutes);
            }
        }
     

        T ICacheService.getFromCache<T>(string key)
        {
            try
            {
                var value = db.StringGet(key);
                if (!value.IsNull)
                    return JsonSerializer.Deserialize<T>(value);
                else
                {
                    return default(T);
                }
             }
            catch (Exception)
            {
                throw;
            }
        }

        void ICacheService.updateCache<T>(string key, T Obj)
        {
            db.StringSet(key, JsonSerializer.Serialize(Obj), expiration);
        }
    }
}
