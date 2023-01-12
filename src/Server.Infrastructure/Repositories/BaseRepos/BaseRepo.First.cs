#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    public TEntity First(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes).First(expression);
    }

    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return await IncludeIfNotNull(includes).FirstAsync(expression, cancellationToken);
    }

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes).FirstOrDefault(expression);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return await IncludeIfNotNull(includes).FirstOrDefaultAsync(expression, cancellationToken);
    }
}