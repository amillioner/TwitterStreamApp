namespace Twitter.StreamApp.Common.Config
{
    public interface IConfigProvider
    {
        ITwitterSettings TwitterSettings { get; }
    }
}
