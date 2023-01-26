#region

using System.Linq.Expressions;

#endregion

namespace Server.Infrastructure.Repositories.Extensions;

public static class RepoExtensions
{
    /// <summary>
    /// If the expression is null, return the query, otherwise return the query filtered by the expression
    /// </summary>
    /// <param name="query">The IQueryable to filter.</param>
    /// <param name="expression">The expression to be used in the Where clause.</param>
    /// <returns>
    /// The query is being returned.
    /// </returns>
    public static IQueryable<TEntity> WhereNullable<TEntity>(this IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>>? expression)
    {
        return expression is null ? query : query.Where(expression);
    }
}