namespace Twitter.StreamApp.Data.Models
{
    public class Entities
    {
        public Hashtag[]? hashtags { get; set; }
        public Mention[]? mentions { get; set; }
        public Url[]? urls { get; set; }
        public Annotation[]? annotations { get; set; }
    }
}