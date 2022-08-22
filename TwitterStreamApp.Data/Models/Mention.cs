namespace Twitter.StreamApp.Data.Models
{
    public class Mention
    {
        public int? start { get; set; }
        public int? end { get; set; }
        public string? username { get; set; }
        public string? id { get; set; }
    }
}