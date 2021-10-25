using StackExchange.Redis;
using System;
using System.Text.Json;

namespace SampleAppTest
{
    public class RedisCacheService<T> : ICacheService<T> where T:class
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        IDatabase db;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
         
            db = _connectionMultiplexer.GetDatabase();
        }

        private static TimeSpan experation = TimeSpan.FromMinutes(2);

        T ICacheService<T>.getFromCache(string key)
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

        void ICacheService<T>.updateCache(string key, T Obj)
        {
            db.StringSet(key, JsonSerializer.Serialize(Obj), experation);
        }
    }
}
