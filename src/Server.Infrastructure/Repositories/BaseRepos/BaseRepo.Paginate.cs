#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Server.Contracts.SubTypes;
using Server.Infrastructure.Repositories.Extensions;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    public (IQueryable<TEntity> Collection, int TotalCount) Paginate(int skipItems, int takeItems, string orderBy,
        OrderDirection orderDirection, Expression<Func<TEntity, bool>>? expression)
    {
        var query = DbSet.WhereNullable(expression);

        query = query.OrderByWithDirection(orderBy, orderDirection);

        return query.PaginateWithTotalCount(skipItems, takeItems);
    }
}