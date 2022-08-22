using Microsoft.Extensions.Logging;
using Twitter.StreamApp.Common.Config;
using Twitter.StreamApp.Data;
using Twitter.StreamApp.Stream.Stream.Base;
using Twitter.StreamApp.Stream.Stream.Base.Subscription;
using Twitter.StreamApp.Stream.Stream.Interfaces;
using Twitter.StreamApp.Stream.Stream.Options;

namespace Twitter.StreamApp.Stream.Stream
{
    public class SampledStreamService : BaseStreamService<Tweet>, ISampledStreamService
    {
        public SampledStreamService(
            ILogger<SampledStreamService> logger
            , ISubscriptionService<Tweet> subscriptionService
            , ISampleStreamOptions streamOptions
            , IConfigProvider config)
            : base(logger, subscriptionService, streamOptions, config,
                filter: x => x?.data?.entities?.hashtags != null)
        {
            StreamUrl = config.TwitterSettings.SampleStreamUrl;
            Fields = config.TwitterSettings.Fields.ToLowerInvariant();
        }
    }
}