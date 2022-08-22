using Twitter.StreamApp.Data.Models;

namespace Twitter.StreamApp.Data
{
    public interface ITweet
    {
        Models.Data? data { get; set; }
        Includes? includes { get; set; }
    }
}