namespace Twitter.StreamApp.Data.Models
{
    public class Annotation
    {
        public int? start { get; set; }
        public int? end { get; set; }
        public float? probability { get; set; }
        public string? type { get; set; }
        public string? normalized_text { get; set; }
    }
}