#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    public async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken)
    {
        return await GetByIdAsync(id, cancellationToken) is not null;
    }

    public async Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return await GetByIdAsync(entity, cancellationToken) is not null;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return await DbSet.AnyAsync(expression, cancellationToken);
    }
}