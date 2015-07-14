
namespace Qz.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.Caching;

    /// <summary>
    /// 缓存类
    /// </summary>
    public class QCache
    {
        private static ObjectCache _cache = new MemoryCache("QCache");

        public static void Set(string key, object value, DateTimeOffset dto)
        {
            _cache.Set(key, value, dto);
        }

        public static void Set(string key, object value, TimeSpan ts)
        {
            Set(key, value, new DateTimeOffset(DateTime.UtcNow + ts));
        }

        public static void Set(string key, object value, int second)
        {
            Set(key, value, TimeSpan.FromSeconds(second));
        }

        public static object Get(string key)
        {
            return _cache[key];
        }

        public static bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }
    }

}
