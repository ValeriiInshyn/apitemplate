#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// > Returns true if the entity with the given id exists, false otherwise
    /// </summary>
    /// <param name="id">The id of the entity to check for existence.</param>
    /// <param name="CancellationToken">This is a token that can be used to cancel the operation.</param>
    /// <returns>
    /// A boolean value.
    /// </returns>
    public async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken)
    {
        return await GetByIdAsync(id, cancellationToken) is not null;
    }

    /// <summary>
    /// > This function returns true if the entity exists in the database, otherwise it returns false
    /// </summary>
    /// <param name="TEntity">The entity type.</param>
    /// <param name="CancellationToken">This is a token that can be used to cancel the operation.</param>
    /// <returns>
    /// A boolean value.
    /// </returns>
    public async Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return await GetByIdAsync(entity, cancellationToken) is not null;
    }

    /// <summary>
    /// It returns a boolean value indicating whether or not the given expression exists in the database
    /// </summary>
    /// <param name="expression">The expression to evaluate.</param>
    /// <param name="CancellationToken">This is a token that can be used to cancel the operation.</param>
    /// <returns>
    /// A boolean value.
    /// </returns>
    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return await DbSet.AnyAsync(expression, cancellationToken);
    }
}