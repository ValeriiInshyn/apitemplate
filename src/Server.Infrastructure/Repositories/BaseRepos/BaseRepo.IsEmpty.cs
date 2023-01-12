#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    public async Task<bool> IsEmptyAsync(CancellationToken cancellationToken)
    {
        return !await DbSet.AnyAsync(cancellationToken);
    }

    public async Task<bool> IsEmptyAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken)
    {
        return !await DbSet.AnyAsync(expression, cancellationToken);
    }
}