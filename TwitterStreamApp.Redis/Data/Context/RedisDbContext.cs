using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;

namespace Twitter.StreamApp.Redis.Data.Context
{
    public class RedisDbContext : IRedisDbContext
    {
        private static ILogger<RedisDbContext> _logger;
        private static RedisDbContext _redisContext;
        private readonly IDatabase _redis;

        public RedisDbContext(ILogger<RedisDbContext> logger)
        {
            _logger = logger;
            _redis = GetRedisDatabase();
        }

        public static RedisDbContext GetInstance() => _redisContext ??= new RedisDbContext(_logger);
        private static IDatabase GetRedisDatabase() => ConnectionMultiplexer.Connect("localhost").GetDatabase();

        private static IDatabase GetRedisDatabase((string, int) endPoints, bool allowAdmin, int connectTimeout)
        {
            var options = new ConfigurationOptions()
            {
                EndPoints = { { "localhost", 6379 } },
                AllowAdmin = true,
                ConnectTimeout = 60 * 1000,
            };
            return ConnectionMultiplexer.Connect(options).GetDatabase(); ;
        }

        IDatabase IRedisDbContext.RedisDb => _redis;

        public void Set<T>(string key, T obj)
        {
            if (typeof(T) == typeof(string))
                _redis.StringSet(key, obj.ToString());
            else
                _redis.HashSet(key, RedisConverter.ToHashEntries(obj));
        }

        public T Get<T>(string key)
        {
            return typeof(T) == typeof(string)
                ? (T)Convert.ChangeType(_redis.StringGet(key), typeof(T))
                : RedisConverter.ConvertFromRedis<T>(_redis.HashGetAll(key));
        }

    }
}