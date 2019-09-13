using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Infrastructure.RedisCache
{
    public class RedisCache<T> : IRedisCache<T>
    {
        private readonly RedisCacheOptions _options;
        private readonly IDatabase _database;
        public RedisCache()
        {
            var redis = ConnectionMultiplexer.Connect(_options.RedisConnectionString);
            _database = redis.GetDatabase();
        }

        private string GetKey(string key) =>
            $"{_options.KeyPrefix}{typeof(T).FullName}:{key}";
        private string GetPatternKey() =>
            $"{_options.KeyPrefix}{typeof(T).FullName}:";

        public async Task RemoveAsync(string key)
        {
            var cacheKey = GetKey(key);
            await _database.KeyDeleteAsync(cacheKey);
        }

        public async Task RemoveAllAsync()
        {
            var pattern = GetPatternKey();
            var server = _database.Multiplexer.GetServer(
                _database.Multiplexer.GetEndPoints().First());
            var keys = server.Keys(pattern: pattern);
            foreach (var key in keys)
                await _database.KeyDeleteAsync(key);
        }
    }
}
