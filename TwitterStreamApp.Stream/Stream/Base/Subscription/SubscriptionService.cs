using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Microsoft.Extensions.Logging;
using Twitter.StreamApp.Cache;
using Twitter.StreamApp.Data;

namespace Twitter.StreamApp.Stream.Stream.Base.Subscription
{
    public class SubscriptionService<T> : ISubscriptionService<T>, IDisposable
        where T : class, ITweet, new()
    {
        private readonly ILogger<SubscriptionService<T>> _logger;
        private readonly ISimpleCacheCalcService _calcService;
        private static readonly object SyncObj = new object();
        private Subject<T> _source;

        public SubscriptionService(
            ILogger<SubscriptionService<T>> logger
            , ISimpleCacheCalcService calcService)
        {
            _logger = logger;
            _calcService = calcService;
        }

        public void StartSubscription()
        {
            _source = new Subject<T>();
            _calcService.Reset();
            long counter = 0;
            _source
                .Where(FilterPredicate)
                .Buffer(TimeSpan.FromSeconds(20))
                .Where(buffer => buffer.Count > 0)
                .Subscribe(onNext: messages =>
                {
                    lock (SyncObj)
                    {
                        Interlocked.Increment(ref counter);
                        counter += messages.Count;
                        _logger.LogInformation($"\n# Total Number of Hashtags processed: {counter} tweets.");
                        _logger.LogInformation($"\n# Processing batch: adding {messages.Count} tweets.");

                        var res = _calcService.Process(messages);
                        //_cacheService.RunCalc(messages);
                        _logger.LogInformation($"\nTop 10 Trending Hashtags:");
                        res.ToList().ForEach(x => _logger.LogInformation($"{x.Item1},{x.Item2}"));
                    }
                }, onError: ex => _logger.LogError(ex.Message));
        }

        public Func<T, bool> FilterPredicate { get; set; }

        public void OnNext(T msg)
        {
            _source?.OnNext(msg);
        }
        public void OnError(Exception ex)
        {
            _source?.OnError(ex);
        }
        public void OnCompleted()
        {
            _source?.OnCompleted();
            _logger.LogInformation("Subscription completed.");
        }
        public void StopSubscription()
        {
            _source?.Dispose();
            _logger.LogInformation("Subscription is disposed.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _source?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}