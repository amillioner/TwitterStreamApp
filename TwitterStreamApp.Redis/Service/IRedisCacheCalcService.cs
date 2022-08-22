using System;
using System.Collections.Generic;
using Twitter.StreamApp.Data;

namespace Twitter.StreamApp.Redis.Service
{
    public interface IRedisCacheCalcService
    {
        void RunCalc2(IList<Tweet> tweets);
        IList<Tuple<long, string>> RunCalc(IList<Tweet> tweets);
    }
}