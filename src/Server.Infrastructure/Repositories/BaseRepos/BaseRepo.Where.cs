#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Server.Contracts.SubTypes;
using Server.Infrastructure.Repositories.Extensions;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return DbSet.Where(expression);
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, string orderBy,
        OrderDirection orderDirection)
    {
        return DbSet.Where(expression).OrderByWithDirection(orderBy, orderDirection);
    }
}