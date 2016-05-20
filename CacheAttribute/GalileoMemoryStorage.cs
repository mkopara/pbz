using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CacheAttribute
{
    /// <summary>
    /// Abstraction of for memory storage actions
    /// </summary>
    internal class GalileoMemoryStorage
    {
        private static readonly MemoryCache Cache = MemoryCache.Default;

        public static void Add(CacheHolder value, string key, DateTime expiration)
        {
            lock (Cache)
            {
                Cache.Add(key, value, expiration);
            }
        }

        public static bool Contains(string key)
        {
            return Cache.Contains(key);
        }
        public static CacheHolder Get(string key)
        {
            var o = Cache.Get(key) as CacheHolder;
            return o;
        }

    }
}
