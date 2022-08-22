using Microsoft.Extensions.Options;
using Twitter.StreamApp.Common.Config;

namespace Twitter.StreamApp.Web.Config
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly TwitterSettings _twitterSettings;

        public ConfigProvider(IOptions<TwitterSettings> twitterSettings)
        {
            _twitterSettings = twitterSettings.Value;
        }

        public ITwitterSettings TwitterSettings => _twitterSettings;
    }
}
