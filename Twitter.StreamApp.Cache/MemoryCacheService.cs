//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Caching.Memory;
//using Twitter.StreamApp.Common;
//using Twitter.StreamApp.Data;

//namespace Twitter.StreamApp.Cache
//{
//    public class MemoryCacheService<K, TItem>
//    where K : class, new()
//    where TItem : class, new()

//    {
//        private readonly IMemoryCache _memoryCache;

//        public MemoryCacheService(IMemoryCache memoryCache)
//        {
//            _memoryCache = memoryCache;
//        }

//        public void Add(K key, TItem value)
//        {
//            var v = _memoryCache.Set(key, value, TimeSpan.FromDays(1));
//        }
//        public void OnGet(K key)
//        {
//            var value = default(TItem);

//            if (!_memoryCache.TryGetValue(key, out var value))
//            {
//                cacheValue = CurrentDateTime;

//                var cacheEntryOptions = new MemoryCacheEntryOptions()
//                    .SetSlidingExpiration(TimeSpan.FromSeconds(3));

//                _memoryCache.Set(CacheKeys.Entry, cacheValue, cacheEntryOptions);
//            }

//            CacheCurrentDateTime = cacheValue;
//        }
//        public void OnGetCacheGetOrCreate()
//        {
//            var cachedValue = _memoryCache.GetOrCreate(
//                CacheKeys.Entry,
//                cacheEntry =>
//                {
//                    cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(3);
//                    return DateTime.Now;
//                });

//            // ...
//        }

//        public async Task OnGetCacheGetOrCreateAsync()
//        {
//            var cachedValue = await _memoryCache.GetOrCreateAsync(
//                CacheKeys.Entry,
//                cacheEntry =>
//                {
//                    cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(3);
//                    return Task.FromResult(DateTime.Now);
//                });

//            // ...
//        }
//    }
//}