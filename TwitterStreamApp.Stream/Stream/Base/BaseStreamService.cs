using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Twitter.StreamApp.Common.Config;
using Twitter.StreamApp.Data;
using Twitter.StreamApp.Data.Request;
using Twitter.StreamApp.Stream.Stream.Base.Subscription;
using Twitter.StreamApp.Stream.Stream.Options;

namespace Twitter.StreamApp.Stream.Stream.Base
{
    public abstract class BaseStreamService<T> : IDisposable
    where T : Tweet, ITweet, new()
    {
        private readonly HttpClient _httpClient;
        private static string _bearerToken;
        private readonly string _rulesStreamUrl;

        protected ILogger Logger { get; private protected set; }
        protected ISubscriptionService<T> SubscriptionService { get; private protected set; }
        protected ICustomRequestOptions Options { get; private protected set; }
        protected string StreamUrl { get; private protected set; }
        protected string Fields { get; private protected set; }

        protected BaseStreamService(
            ILogger logger
            , ISubscriptionService<T> subscriptionService
            , ICustomRequestOptions options
            , IConfigProvider config
            , Func<T, bool> filter)
        {
            Logger = logger;
            SubscriptionService = subscriptionService;
            SubscriptionService.FilterPredicate = filter;
            Options = options;
            _bearerToken = config.TwitterSettings.BearerToken;
            _rulesStreamUrl = config.TwitterSettings.RulesStreamUrl;
            _httpClient = InitializeHttpClient();
        }
        private static HttpClient InitializeHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
        public virtual async Task StartStream(CancellationToken token)
        {
            try
            {
                SubscriptionService.StartSubscription();
                using (var stream = await _httpClient.GetStreamAsync($"{StreamUrl}?{Fields}", token).ConfigureAwait(false))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            if (token.IsCancellationRequested)
                            {
                                SubscriptionService.OnCompleted();
                                return;
                            }
                            try
                            {
                                var line = await reader.ReadLineAsync().ConfigureAwait(false);
                                //_logger.LogInformation(line);

                                if (!string.IsNullOrWhiteSpace(line))
                                {
                                    var tweet = System.Text.Json.JsonSerializer.Deserialize<T>(line);
                                    SubscriptionService.OnNext(tweet);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.LogError(ex.Message);
                                SubscriptionService.OnError(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString()); 
                SubscriptionService.OnError(ex);
            }
            finally
            {
                SubscriptionService.StopSubscription();
            }
        }
        public virtual void StopStream(CancellationTokenSource cancellationSource)
        {
            cancellationSource.Cancel();
            //cancellationSource.Dispose();
            SubscriptionService.StopSubscription();
        }
        public virtual async Task<System.IO.Stream> AddRules(AddRulesRequest addRules)
        {
            System.IO.Stream result = null;
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_rulesStreamUrl, addRules).ConfigureAwait(false); ;
                if (response.IsSuccessStatusCode)
                    result = await response.Content.ReadAsStreamAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            return result;
        }
        public virtual async Task<System.IO.Stream> RetrieveRules()
        {
            System.IO.Stream result = null;
            try
            {
                result = await _httpClient.GetStreamAsync(_rulesStreamUrl).ConfigureAwait(false); ;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            return result;
        }
        public virtual async Task<System.IO.Stream> DeleteRules(DeleteRulesRequest deleteRules)
        {
            System.IO.Stream result = null;
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_rulesStreamUrl, deleteRules).ConfigureAwait(false); ;
                if (response.IsSuccessStatusCode)
                    result = await response.Content.ReadAsStreamAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
                SubscriptionService.StopSubscription();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}