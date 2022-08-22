namespace Twitter.StreamApp.Data.Request
{
    public class DeleteRulesRequest
    {
        public Delete? delete { get; set; }
    }

    public class Delete
    {
        public string[]? ids { get; set; }
    }

}