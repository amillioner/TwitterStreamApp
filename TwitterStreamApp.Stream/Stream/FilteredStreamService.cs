using Microsoft.Extensions.Logging;
using Twitter.StreamApp.Common.Config;
using Twitter.StreamApp.Data;
using Twitter.StreamApp.Stream.Stream.Base;
using Twitter.StreamApp.Stream.Stream.Base.Subscription;
using Twitter.StreamApp.Stream.Stream.Interfaces;
using Twitter.StreamApp.Stream.Stream.Options;

namespace Twitter.StreamApp.Stream.Stream
{
    public class FilteredStreamService : BaseStreamService<Tweet>, IFilteredStreamService
    {
        public FilteredStreamService(
            ILogger<FilteredStreamService> logger
            , ISubscriptionService<Tweet> subscriptionService
            , IFilteredStreamOptions streamOptions
            , IConfigProvider config)
            : base(logger, subscriptionService, streamOptions, config,
            filter: x => x?.data?.entities?.hashtags != null)
        {
            StreamUrl = config.TwitterSettings.FilteredStreamUrl;
            Fields = config.TwitterSettings.Fields.ToLowerInvariant();
        }
    }
}