using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    internal static class LinqExtensions
    {
        public static bool In<T>(this T @this, Func<T, T, bool> comparer, params T[] range)
            => In<T>(@this, range, comparer);

        public static bool In<T>(this T @this, IEnumerable<T> range, Func<T, T, bool> comparer = null)
            => range?.Any(e => comparer?.Invoke(@this, e) ?? Equals(@this, e)) == true;


        public static IDictionary<T, object> OfKeyType<T>(this IDictionary dictionary)
        {
            if (dictionary == null) return null;
            var allKeys = dictionary.Keys.OfType<T>().ToList();
            return allKeys.ToDictionary(key => key, key => dictionary[key]);
        }

        public static IDictionary<TKey, TValue> OfValueType<TKey, TValue>(this IDictionary<TKey, object> dictionary)
        {
            if (dictionary == null) return null;
            var allValues = dictionary.Values.OfType<TValue>().ToList();
            var dic = new Dictionary<TKey, TValue>();

            foreach (var value in allValues)
            {
                if (dic.ContainsValue(value))
                {
                    continue;
                }
                foreach (var key in dictionary.Keys)
                {
                    if (dic.ContainsKey(key))
                    {
                        continue;
                    }
                    var objectValue = dictionary[key];
                    if (ReferenceEquals(objectValue, value)
                        || Equals(objectValue, value))
                    {
                        dic.Add(key, value);
                    }
                }
            }

            return dic;
        }

        public static T FirstOrDefault<T>(this IDictionary dictionary, string key)
        {
            if (dictionary == null) return default(T);
            return dictionary
                .OfKeyType<string>()
                .OfValueType<string, T>()
                .FirstOrDefault(x => 
                    string.Equals(key, x.Key, 
                    StringComparison.InvariantCultureIgnoreCase))
                .Value;
        }
    }
}
