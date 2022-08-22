using StackExchange.Redis;

namespace Twitter.StreamApp.Redis.Data.Context
{
    public interface IRedisDbContext
    {
        IDatabase RedisDb { get; }
        void Set<T>(string key, T obj);
        T Get<T>(string key);
    }
}