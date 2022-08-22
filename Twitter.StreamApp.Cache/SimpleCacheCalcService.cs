using System;
using System.Collections.Generic;
using System.Linq;
using Twitter.StreamApp.Common;
using Twitter.StreamApp.Data;

namespace Twitter.StreamApp.Cache
{
    public class SimpleCacheCalcService : ISimpleCacheCalcService
    {
        private const int TopTrendingTweetsByHashtag = 10;
        private IDictionary<string, long> _frequencyMap = new Dictionary<string, long>();

        public void Reset()
        {
            _frequencyMap = new Dictionary<string, long>();
        }
        public IList<Tuple<long, string>> Process<T>(IList<T> tweets) where T : class, ITweet, new()
        {
            CountWordFrequency(tweets);

            var result = SortWordsAndCompareFrequencies();
            return result;
        }

        private void CountWordFrequency<T>(IEnumerable<T> tweets) where T : class, ITweet, new()
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
        }

        private IList<Tuple<long, string>> SortWordsAndCompareFrequencies()
        {
            var sortedWords = new SortedSet<string>(_frequencyMap.Keys, new WordAndFrequencyComparer(_frequencyMap));

            var result = new List<Tuple<long, string>>();
            foreach (var word in sortedWords.TakeWhile(word => result.Count != TopTrendingTweetsByHashtag))
            {
                result.Add(new Tuple<long, string>(_frequencyMap[word], word));
            }

            return result;
        }

    }
}
