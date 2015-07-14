using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace Qz.Caching
{
    /// <summary>
    /// 缓存类
    /// </summary>
    public sealed class MCache
    {
        private readonly static MemoryCache cache = new MemoryCache("MemoryCache");

        /// <summary>
        /// 向缓存中插入缓存项
        /// </summary>
        /// <param name="key">该缓存项的唯一标识符。</param>
        /// <param name="value">要插入的对象。</param>
        /// <param name="absoluteExpiration">缓存项的固定的过期日期和时间。</param>
        public static void Add(string key, object value, DateTimeOffset absoluteExpiration)
        {
            cache.Add(key, value, absoluteExpiration);
        }


        /// <summary>
        /// 向缓存中插入缓存项
        /// </summary>
        /// <param name="key">该缓存项的唯一标识符。</param>
        /// <param name="value">要插入的对象。</param>
        /// <param name="minutesValue">分钟数，精确到最接近的毫秒。一个时段，必须在此时段内访问某个缓存项，否则将从内存中逐出该缓存项。</param>
        public static bool Add(string key, object value, double minutesValue)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = TimeSpan.FromMinutes(minutesValue);
            return cache.Add(key, value, policy);
        }

        /// <summary>
        /// 向缓存中插入缓存项
        /// </summary>
        /// <param name="key">该缓存项的唯一标识符。</param>
        /// <param name="value">要插入的对象。</param>
        /// <param name="secondsValue">秒数，精确到最接近的毫秒。一个时段，必须在此时段内访问某个缓存项，否则将从内存中逐出该缓存项。</param>
        public static bool Add(string key, object value, int secondsValue)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = TimeSpan.FromSeconds(secondsValue);
            return cache.Add(key, value, policy);
        }

        /// <summary>
        /// 向缓存中插入缓存项
        /// </summary>
        /// <param name="key">该缓存项的唯一标识符。</param>
        /// <param name="value">要插入的对象。</param>
        /// <param name="minutesValue">分钟数，精确到最接近的毫秒。一个时段，必须在此时段内访问某个缓存项，否则将从内存中逐出该缓存项。</param>
        /// <param name="removedCallback">在从缓存中移除某个缓存项后将调用该方法。</param>
        public static bool Add(string key, object value, double minutesValue, CacheEntryRemovedCallback RemovedCallback)
        {
            CacheItem item = new CacheItem(key, value);
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = TimeSpan.FromMinutes(minutesValue);
            policy.RemovedCallback = RemovedCallback;
            return cache.Add(item, policy);
        }
        /// <summary>
        /// 向缓存中设置缓存项
        /// </summary>
        /// <param name="key">该缓存项的唯一标识符。</param>
        /// <param name="value">要插入的对象。</param>
        /// <param name="minutesValue">分钟数，精确到最接近的毫秒。一个时段，必须在此时段内访问某个缓存项，否则将从内存中逐出该缓存项。</param>
        /// <param name="removedCallback">在从缓存中移除某个缓存项后将调用该方法。</param>
        public static void Set(string key, object value, double minutesValue, CacheEntryRemovedCallback RemovedCallback)
        {
            CacheItem item = new CacheItem(key, value);
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = TimeSpan.FromMinutes(minutesValue);
            policy.RemovedCallback = RemovedCallback;
            cache.Set(item, policy);
        }


        /// <summary>
        /// 从缓存中返回一个项
        /// </summary>
        /// <param name="key">该缓存项的唯一标识符。</param>
        /// <returns>如果该项存在，则为对 key 标识的缓存项的引用；否则为 null。</returns>
        public static object Get(string key)
        {
            return cache.Get(key);
        }

        /// <summary>
        /// 使用键和值将某个缓存项插入缓存中，并指定基于时间的过期详细信息。
        /// </summary>
        /// <param name="key">该缓存项的唯一标识符。</param>
        /// <param name="value">要插入的对象。</param>
        /// <param name="absoluteExpiration">缓存项的固定的过期日期和时间。</param>
        public static void Set(string key, object value, DateTimeOffset absoluteExpiration)
        {
            cache.Set(key, value, absoluteExpiration);
        }

        /// <summary>
        /// 使用键和值将某个缓存项插入缓存中，并指定基于时间的过期详细信息。
        /// </summary>
        /// <param name="key">该缓存项的唯一标识符。</param>
        /// <param name="value">要插入的对象。</param>
        /// <param name="minutesValue">分钟数，精确到最接近的毫秒。一个时段，必须在此时段内访问某个缓存项，否则将从内存中逐出该缓存项。</param>
        public static void Set(string key, object value, double minutesValue)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = TimeSpan.FromMinutes(minutesValue);
            cache.Set(key, value, policy);
        }

        /// <summary>
        /// 从缓存中移除某个缓存项。
        /// </summary>
        /// <param name="key">该缓存项的唯一标识符。</param>
        public static void Remove(string key)
        {
            cache.Remove(key);
        }
    }
}
