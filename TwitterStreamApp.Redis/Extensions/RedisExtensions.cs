using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using Twitter.StreamApp.Redis.Data.Context;
using Twitter.StreamApp.Redis.Service;

namespace Twitter.StreamApp.Redis.Extensions
{
    public static class RedisExtensions
    {
        public static void RegisterRedisDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(options =>
                new[] { config.GetSection("Redis").Get<RedisConfiguration>() });

            //Configure other services up here
            var multiplexer = ConnectionMultiplexer.Connect("localhost");
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            services.AddSingleton<IRedisDbContext, RedisDbContext>();
            services.AddSingleton<IRedisCacheCalcService, RedisCacheCalcService>();
        }
    }
}