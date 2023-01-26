#region

using System.Linq.Expressions;
using Server.Contracts.SubTypes;

#endregion

namespace Server.Infrastructure.Repositories.Extensions;

public static class OrderByExtensions
{
    /// <summary>
    /// It takes a query, a property name, and an order direction, and returns a new query with the order applied
    /// </summary>
    /// <param name="query">The query to be ordered</param>
    /// <param name="propertyName">The name of the property to order by.</param>
    /// <param name="OrderDirection">An enum that can be either Asc or Desc</param>
    /// <returns>
    /// An IOrderedQueryable<TEntity>
    /// </returns>
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


    /// <summary>
    /// It takes a query and an OrderByData object, and returns the query ordered by the property specified in the
    /// OrderByData object, in the direction specified in the OrderByData object
    /// </summary>
    /// <param name="query">The query to be ordered.</param>
    /// <param name="OrderByData">This is a class that contains the OrderBy and OrderDirection properties.</param>
    /// <returns>
    /// An IOrderedQueryable<TEntity>
    /// </returns>
    public static IOrderedQueryable<TEntity> OrderByWithDirection<TEntity>(
        this IQueryable<TEntity> query, OrderByData orderByData) where TEntity : class
    {
        return query.OrderByWithDirection(orderByData.OrderBy, orderByData.OrderDirection);
    }


    /// <summary>
    /// If the order direction is ascending, order the query by the key selector, otherwise order the query by the key
    /// selector in descending order
    /// </summary>
    /// <param name="query">The query to be ordered.</param>
    /// <param name="keySelector">The property to order by.</param>
    /// <param name="OrderDirection">An enum that has two values, Asc and Desc.</param>
    /// <returns>
    /// An IOrderedQueryable<TEntity>
    /// </returns>
    public static IOrderedQueryable<TEntity> OrderByWithDirection<TEntity>(
        this IQueryable<TEntity> query, Expression<Func<TEntity, object>> keySelector, OrderDirection orderDirection)
    {
        return orderDirection == OrderDirection.Asc ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);
    }
}