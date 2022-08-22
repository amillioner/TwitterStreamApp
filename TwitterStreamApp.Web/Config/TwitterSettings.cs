using Twitter.StreamApp.Common.Config;

namespace Twitter.StreamApp.Web.Config
{
    public class TwitterSettings : ITwitterSettings
    {
        public string BearerToken { get; set; }
        public string Fields { get; set; }
        public string SampleStreamUrl { get; set; }
        public string FilteredStreamUrl { get; set; }
        public string RulesStreamUrl { get; set; }
    }
}
