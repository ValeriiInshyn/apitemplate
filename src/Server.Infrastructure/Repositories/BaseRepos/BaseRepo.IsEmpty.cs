#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// > Returns true if the database table is empty, otherwise returns false
    /// </summary>
    /// <param name="CancellationToken">This is a token that can be used to cancel the operation.</param>
    /// <returns>
    /// A boolean value.
    /// </returns>
    public async Task<bool> IsEmptyAsync(CancellationToken cancellationToken)
    {
        return !await DbSet.AnyAsync(cancellationToken);
    }

    /// <summary>
    /// > It returns true if the database table is empty, otherwise it returns false
    /// </summary>
    /// <param name="expression">The expression to evaluate.</param>
    /// <param name="CancellationToken">This is a token that can be used to cancel the operation.</param>
    /// <returns>
    /// A boolean value.
    /// </returns>
    public async Task<bool> IsEmptyAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken)
    {
        return !await DbSet.AnyAsync(expression, cancellationToken);
    }
}