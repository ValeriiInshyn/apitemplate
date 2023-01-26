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
    /// It takes a skipItems, takeItems, orderBy, orderDirection, and expression, and returns a tuple of the paginated query
    /// and the total count of the query
    /// </summary>
    /// <param name="skipItems">The number of items to skip.</param>
    /// <param name="takeItems">The number of items to take from the collection.</param>
    /// <param name="orderBy">The name of the property to order by.</param>
    /// <param name="OrderDirection">An enum that can be either Ascending or Descending.</param>
    /// <param name="expression">This is the filter expression.</param>
    /// <returns>
    /// A tuple of IQueryable<TEntity> and int
    /// </returns>
    public (IQueryable<TEntity> Collection, int TotalCount) Paginate(int skipItems, int takeItems, string orderBy,
        OrderDirection orderDirection, Expression<Func<TEntity, bool>>? expression)
    {
        var query = DbSet.WhereNullable(expression);

        query = query.OrderByWithDirection(orderBy, orderDirection);

        return query.PaginateWithTotalCount(skipItems, takeItems);
    }
}