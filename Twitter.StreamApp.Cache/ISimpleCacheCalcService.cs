using System;
using System.Collections.Generic;
using Twitter.StreamApp.Data;

namespace Twitter.StreamApp.Cache
{
    public interface ISimpleCacheCalcService
    {
        void Reset();
        IList<Tuple<long, string>> Process<T>(IList<T> tweets) where T : class, ITweet, new();
    }
}