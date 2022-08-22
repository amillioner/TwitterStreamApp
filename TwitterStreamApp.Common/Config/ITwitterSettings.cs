namespace Twitter.StreamApp.Common.Config
{
    public interface ITwitterSettings
    {
        string BearerToken { get; set; }
        string Fields { get; set; }
        string SampleStreamUrl { get; set; }
        string FilteredStreamUrl { get; set; }
        string RulesStreamUrl { get; set; }
    }
}