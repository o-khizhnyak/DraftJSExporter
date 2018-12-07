using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace DraftJSExporter
{
    public static class PropertyExpression<T>
    {
        private static readonly ConcurrentDictionary<string, Func<T, object>> Cache =
            new ConcurrentDictionary<string, Func<T, object>>(); 

        public static object GetValue(string propertyName)
        {
            return Cache.GetOrAdd(propertyName, key =>
            {
                var parameter = Expression.Parameter(typeof(T));
                var f = Expression.Lambda<Func<T, object>>(
                    Expression.Convert(Expression.Property(parameter, key), typeof(object)), parameter).Compile();
                return f;
            });
        }
    }
}