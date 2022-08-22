namespace Twitter.StreamApp.Data.Models
{
    public class Url
    {
        public int? start { get; set; }
        public int? end { get; set; }
        public string? url { get; set; }
        public string? expanded_url { get; set; }
        public string? display_url { get; set; }
        public Image[]? images { get; set; }
        public int? status { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? unwound_url { get; set; }
        public string? media_key { get; set; }
    }
}