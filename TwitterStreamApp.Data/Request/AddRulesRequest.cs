namespace Twitter.StreamApp.Data.Request
{
    public class AddRulesRequest
    {
        public Add[]? add { get; set; }
    }

    public class Add
    {
        public string? value { get; set; }
        public string? tag { get; set; }
    }
}