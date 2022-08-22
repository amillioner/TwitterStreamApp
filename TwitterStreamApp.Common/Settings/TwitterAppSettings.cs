namespace Twitter.StreamApp.Common.Settings
{
    public enum RateLimitTrackerMode
    {
        None,
        TrackAndAwait,
        TrackOnly,
    }

    public enum TweetMode
    {
        Extended = 0,
        Compat = 1,
        None = 2
    }
}