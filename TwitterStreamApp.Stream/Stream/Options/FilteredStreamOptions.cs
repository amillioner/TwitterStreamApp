using Twitter.StreamApp.Common.Settings;

namespace Twitter.StreamApp.Stream.Stream.Options
{
    public interface IFilteredStreamOptions : ICustomRequestOptions
    {
        TweetMode? TweetMode { get; set; }
    }

    public class FilteredStreamOptions : CustomRequestOptions, IFilteredStreamOptions
    {
        public TweetMode? TweetMode { get; set; }
    }
}