using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheAttribute
{
    /// <summary>
    /// Abstraction For public usage and in memory caching
    /// </summary>
    public static class InMemoryCache
    {
        private static readonly object locker = new object();
        public static T GetOrSet<T>(string key, Func<T> getItemCallback, int seconds = 60) where T : class
        { 
            var item = System.Runtime.Caching.MemoryCache.Default.Get(key) as T;
            if (item == null)
            {
                //if item does not exist, invoke callback to load it
                item = getItemCallback?.Invoke();
                lock (locker)
                {
                    System.Runtime.Caching.MemoryCache.Default.Add(key, item, DateTime.Now.AddSeconds(seconds));
                }
               
            }
            return item;
        }

        public static void ClearKey(string key)
        {
            System.Runtime.Caching.MemoryCache.Default.Remove(key);
        }

        public static void ClearAll()
        {
            foreach (var cache in System.Runtime.Caching.MemoryCache.Default)
                System.Runtime.Caching.MemoryCache.Default.Remove(cache.Key);
        }
    }
}
