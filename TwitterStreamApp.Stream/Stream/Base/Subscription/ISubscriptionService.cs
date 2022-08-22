using System;
using Twitter.StreamApp.Data;

namespace Twitter.StreamApp.Stream.Stream.Base.Subscription
{
    public interface ISubscriptionService<T>
        where T : class, ITweet, new()
    {
        void StartSubscription();
        void StopSubscription();
        void OnNext(T msg);
        void OnError(Exception ex);
        void OnCompleted();
        Func<T, bool> FilterPredicate { get; set; }
    }
}