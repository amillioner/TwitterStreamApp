using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using Twitter.StreamApp.Common;
using Twitter.StreamApp.Data;
using Twitter.StreamApp.Redis.Data.Context;

namespace Twitter.StreamApp.Redis.Service
{
    public class RedisCacheCalcService : IRedisCacheCalcService
    {
        private readonly IDatabase _redis;

        public RedisCacheCalcService(IRedisDbContext redis)
        {
            _redis = redis.RedisDb;
        }
        private int k = 10;
        private readonly IDictionary<string, long> _frequencyMap = new Dictionary<string, long>();

        public void RunCalc2(IList<Tweet> tweets)
        {
            foreach (var tweet in tweets)
            {
                if (tweet.data?.entities?.hashtags == null) continue;
                foreach (var hashTag in tweet.data?.entities?.hashtags)
                {
                    if (string.IsNullOrWhiteSpace(hashTag.tag)) continue;
                    var hashKey = hashTag.tag;
                    var word = hashTag.tag;
                    //_frequencyMap.TryGetValue(word, out long count);
                    //_frequencyMap[word] = count + 1;

                    if (_redis.HashExists(hashKey, word))
                        _redis.HashIncrement(hashKey, word, 1); //increment by 1
                    else
                        _redis.HashSet(hashKey, word, 1);
                }
            }
        }
        public IList<Tuple<long, string>> RunCalc(IList<Tweet> tweets)
        {
            foreach (var tweet in tweets)
            {
                if (tweet.data?.entities?.hashtags == null) continue;
                foreach (var hashTag in tweet.data?.entities?.hashtags)
                {
                    if (string.IsNullOrWhiteSpace(hashTag.tag)) continue;
                    var word = hashTag.tag;
                    _frequencyMap.TryGetValue(word, out long count);
                    _frequencyMap[word] = count + 1;
                }
            }

            var sortedWords = new SortedSet<string>(_frequencyMap.Keys, new WordAndFrequencyComparer(_frequencyMap));

            var result = new List<Tuple<long, string>>();
            foreach (var word in sortedWords.TakeWhile(word => result.Count != k))
            {
                result.Add(new Tuple<long, string>(_frequencyMap[word], word));
            }
            return result;
        }
    }
}
