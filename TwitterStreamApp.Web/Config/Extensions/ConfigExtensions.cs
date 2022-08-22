using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Twitter.StreamApp.Common.Config;

namespace Twitter.StreamApp.Web.Config.Extensions
{
    public static class ConfigExtensions
    {
        public static void RegisterConfigSettings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<TwitterSettings>(config.GetSection("TwitterSettings"));

            services.AddSingleton<IConfigProvider, ConfigProvider>();
            services.AddSingleton<ITwitterSettings, TwitterSettings>();

        }
    }
}
