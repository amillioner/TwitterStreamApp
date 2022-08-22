using System;

namespace Twitter.StreamApp.Data.Models
{
    public class Data
    {
        public string? id { get; set; }
        public string? text { get; set; }
        public bool? possibly_sensitive { get; set; }
        public Referenced_Tweets[]? referenced_tweets { get; set; }
        public Entities? entities { get; set; }
        public string? author_id { get; set; }
        public Public_Metrics? public_metrics { get; set; }
        public string? lang { get; set; }
        public DateTime? created_at { get; set; }
        public string? source { get; set; }
        public string? in_reply_to_user_id { get; set; }
        public Attachments? attachments { get; set; }
        public Context_Annotations[]? context_annotations { get; set; }
    }
}