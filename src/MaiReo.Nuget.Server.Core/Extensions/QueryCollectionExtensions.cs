using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// A ugly model binder.
    /// Any suggestion(s)?
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class QueryCollectionExtensions
    {
        private static readonly
            ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> _prop_cache;

        static QueryCollectionExtensions()
        {
            _prop_cache
            = new ConcurrentDictionary<Type, IEnumerable<PropertyInfo>>();
        }

        public static T As<T>(this IQueryCollection query) where T : class, new()
        {
            var props = _prop_cache.GetOrAdd(typeof(T),
            type => type.GetProperties().Where(p => p.CanWrite).ToList());

            var joined = props.Select(p => new
            {
                p.SetMethod,
                p.PropertyType,
                Value = query[p.Name]
            })
            .Where(x => x.Value != default(StringValues) && x.Value.Any())
            .ToList();
            var bindings = new List<MemberAssignment>();
            foreach (var item in joined)
            {
                if (typeof(IEnumerable).IsAssignableFrom(item.PropertyType)
                    && !typeof(IEnumerable<char>).IsAssignableFrom(item.PropertyType)
                    )
                {
                    //Skip binding non-char array
                    continue;
                }
                var stringValue = item.Value.Last();
                if (item.PropertyType == typeof(string))
                {
                    bindings.Add(Expression.Bind(
                        item.SetMethod,
                        Expression.Constant(stringValue, typeof(string))));
                    continue;
                }
                var conv = TypeDescriptor.GetConverter(item.PropertyType);
                if (!conv.CanConvertFrom(typeof(string)))
                {
                    continue;
                }

                if (!conv.IsValid(stringValue))
                {
                    continue;
                }
                var objectValue = conv.ConvertFromString(stringValue);
                bindings.Add(Expression.Bind(item.SetMethod, Expression.Constant(objectValue, item.PropertyType)));
            }
            var lambda = Expression.Lambda<Func<T>>(Expression.MemberInit(Expression.New(typeof(T)), bindings));
            return lambda.Compile().Invoke();
        }
    }
}
