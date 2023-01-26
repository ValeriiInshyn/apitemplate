#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Server.Contracts.SubTypes;
using Server.Infrastructure.Repositories.Extensions;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// It takes a lambda expression as a parameter and returns an IQueryable object
    /// </summary>
    /// <param name="expression">The expression to be evaluated.</param>
    /// <returns>
    /// IQueryable<TEntity>
    /// </returns>
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.Where(expression);
    }

    /// <summary>
    /// It takes an expression, an orderBy string, and an orderDirection enum, and returns an IQueryable of TEntity
    /// </summary>
    /// <param name="expression">The expression to filter the results by.</param>
    /// <param name="orderBy">The name of the property to order by.</param>
    /// <param name="OrderDirection">An enum that can be either Ascending or Descending</param>
    /// <returns>
    /// IQueryable<TEntity>
    /// </returns>
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, string orderBy,
        OrderDirection orderDirection)
    {
        return DbSet.Where(expression).OrderByWithDirection(orderBy, orderDirection);
    }
}