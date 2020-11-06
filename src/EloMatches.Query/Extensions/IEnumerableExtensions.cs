using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EloMatches.Query.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IEnumerableExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IEnumerable<TSource> query, string propertyName)
        {
            return query.CreateOrderedQueryableExpression("OrderBy", propertyName);
        }

        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> query, string propertyName)
        {
            return query.CreateOrderedQueryableExpression("OrderByDescending", propertyName);
        }

        private static IOrderedQueryable<TSource> CreateOrderedQueryableExpression<TSource>(this IEnumerable<TSource> query, string orderByMethodName, string propertyName)
        {
            var entityType = typeof(TSource);

            var propertyInfo = entityType.GetProperty(propertyName);
            if (propertyInfo == null)
                throw new ArgumentException($"Property '{propertyName}' not found on type '{entityType.Name}'");

            var parameterExpression = Expression.Parameter(entityType, "x");
            var memberExpression = Expression.Property(parameterExpression, propertyName);

            var selector = Expression.Lambda(memberExpression, parameterExpression);

            var orderByMethod = typeof(Queryable).GetMethods().Single(x => x.Name == orderByMethodName && x.IsGenericMethodDefinition && x.GetParameters().Length == 2);

            var genericOrderByMethod = orderByMethod.MakeGenericMethod(entityType, propertyInfo.PropertyType);

            return (IOrderedQueryable<TSource>) genericOrderByMethod.Invoke(genericOrderByMethod, new object[]{query, selector});
        }
    }
}