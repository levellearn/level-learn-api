using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LevelLearn.Domain.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string columnName, bool isAscending = true)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(columnName)) return source;

            PropertyInfo propertyInfo = typeof(T).GetProperty(columnName);

            if (propertyInfo == null) return source;

            ParameterExpression parameter = Expression.Parameter(source.ElementType, "");

            MemberExpression property = Expression.Property(parameter, columnName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isAscending ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                  new Type[] { source.ElementType, property.Type },
                                  source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

    }
}
