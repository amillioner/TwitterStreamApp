using Twitter.StreamApp.Data.Models;

namespace Twitter.StreamApp.Data
{
    public class Tweet : ITweet
    {
        public Models.Data? data { get; set; }
        public Includes? includes { get; set; }
    }
}