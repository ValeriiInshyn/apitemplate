#region

using System.Linq.Expressions;
using Server.Contracts.SubTypes;

#endregion

namespace Server.Infrastructure.Repositories.Extensions;

public static class OrderByExtensions
{
    public static IOrderedQueryable<TEntity> OrderByWithDirection<TEntity>(
        this IQueryable<TEntity> query, string propertyName, OrderDirection orderDirection) where TEntity : class
    {
        var entityType = typeof(TEntity);
        var propertyInfo = entityType.GetProperty(propertyName); //Should implement exception here

        var arg = Expression.Parameter(entityType, "x");
        var property = Expression.Property(arg, propertyName);

        var selector = Expression.Lambda(property, arg);
        var enumerableType = typeof(Queryable);
        var method = enumerableType.GetMethods()
            .Where(m => m.Name == (orderDirection == OrderDirection.Asc ? "OrderBy" : "OrderByDescending") &&
                        m.IsGenericMethodDefinition)
            .Single(m =>
            {
                var parameters = m.GetParameters().ToList();
                return parameters.Count == 2;
            });
        var genericMethod = method
            .MakeGenericMethod(entityType, propertyInfo?.PropertyType!);

        var newQuery = (IOrderedQueryable<TEntity>)genericMethod
            .Invoke(genericMethod, new object[] { query, selector })!;
        return newQuery;
    }


    public static IOrderedQueryable<TEntity> OrderByWithDirection<TEntity>(
        this IQueryable<TEntity> query, OrderByData orderByData) where TEntity : class
    {
        return query.OrderByWithDirection(orderByData.OrderBy, orderByData.OrderDirection);
    }


    public static IOrderedQueryable<TEntity> OrderByWithDirection<TEntity>(
        this IQueryable<TEntity> query, Expression<Func<TEntity, object>> keySelector, OrderDirection orderDirection)
    {
        return orderDirection == OrderDirection.Asc ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
    }
}