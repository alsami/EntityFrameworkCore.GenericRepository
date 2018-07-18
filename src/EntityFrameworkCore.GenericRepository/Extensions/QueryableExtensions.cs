using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EntityFrameworkCore.GenericRepository.Shared;

namespace EntityFrameworkCore.GenericRepository.Extensions
{
    internal static class QueryableExtensions
    {
        public const string OrderByMethodName = "OrderBy";
        public const string ThenOrderByMethodName = "ThenOrderBy";
        public const string OrderByDescendingMethodName = "OrderByDescending";
        public const string ThenOrderByDescendingMethodName = "ThenOrderDescendingBy";

        internal static IOrderedQueryable<TEntity> ApplyOrder<TEntity, TId>(this IQueryable<TEntity> source, 
            string property, string methodName = QueryableExtensions.OrderByMethodName)  where TEntity : class, IEntity<TId>, new() where TId : IEquatable<TId>
        {
            var props = property.Split('.');

            var type = typeof(TEntity);

            var arg = Expression.Parameter(type, type.GetTypeInfo().Name);
            Expression expr = arg;

            foreach (var prop in props)
            {
                var propertyInfo = type.GetProperty(prop);

                if (propertyInfo == null)
                {
                    continue;
                }
                
                expr = Expression.Property(expr, propertyInfo);
                type = propertyInfo.PropertyType;
            }

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(TEntity), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            var orderedQueryable = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                              && method.IsGenericMethodDefinition
                              && method.GetGenericArguments().Length == 2
                              && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TEntity), type)
                .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<TEntity>) orderedQueryable;
        }
    }
}