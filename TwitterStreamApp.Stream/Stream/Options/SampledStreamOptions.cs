using Twitter.StreamApp.Common.Settings;

namespace Twitter.StreamApp.Stream.Stream.Options
{
    public interface ISampleStreamOptions : ICustomRequestOptions
    {
        TweetMode? TweetMode { get; set; }
    }

    public class SampleStreamOptions : CustomRequestOptions, ISampleStreamOptions
    {
        public TweetMode? TweetMode { get; set; }
    }
}