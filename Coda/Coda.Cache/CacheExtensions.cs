using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace Coda.Cache
{
    public static class CacheExtensions
    {
        public static TItem ManagedSet<TItem>(this IMemoryCache cache, string category, string key, TItem value)
        {
            cache.AddManagedCategoryKey(category, key);
            cache.Set(GetFullKey(category, key), value);
            return value;
        }

        public static TItem ManagedSet<TItem>(this IMemoryCache cache, string category, string key, TItem value, DateTimeOffset absoluteExpiration)
        {
            cache.AddManagedCategoryKey(category, key);
            cache.Set(GetFullKey(category, key), value, absoluteExpiration);
            return value;
        }

        public static bool TryGetManagedValue<TItem>(this IMemoryCache cache, string category, string key, out TItem value)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            if (key == null) throw new ArgumentNullException(nameof(key));

            return cache.TryGetValue(GetFullKey(category, key), out value);
        }

        public static void ClearManagedCategory(this IMemoryCache cache, string category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            // Don't use GetManagedCategory to handle if the key exists or not
            if (cache.TryGetValue(GetCategoryKey(category), out HashSet<string> keys))
            {
                foreach(var key in keys)
                {
                    cache.Remove(key);
                }
                cache.Remove(GetCategoryKey(category));
            }
        }
        
        private static HashSet<string> GetManagedCategory(this IMemoryCache cache, string category)
        {
            if (cache.TryGetValue(GetCategoryKey(category), out HashSet<string> keys))
            {
                return keys;
            }
            return new HashSet<string>();
        }

        private static void AddManagedCategoryKey(this IMemoryCache cache, string category, string key)
        {
            var currentKeys = cache.GetManagedCategory(category);
            currentKeys.Add(GetFullKey(category, key));
            cache.Set(GetCategoryKey(category), currentKeys, new MemoryCacheEntryOptions { Priority = CacheItemPriority.NeverRemove });
        }

        private static string GetCategoryKey(string category) => $"M{category}[.]";

        private static string GetFullKey(string category, string key) => $"M{category}[{key}]";
    }
}
