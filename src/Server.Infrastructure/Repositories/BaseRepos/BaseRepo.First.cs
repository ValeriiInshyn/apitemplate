#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// > This function returns the first entity that matches the given expression
    /// </summary>
    /// <param name="expression">The expression to filter the results by.</param>
    /// <param name="includes">This is an optional parameter that allows you to include related entities in the
    /// query.</param>
    /// <returns>
    /// The first entity that matches the expression.
    /// </returns>
    public TEntity First(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes).First(expression);
    }

    /// <summary>
    /// > This function returns the first entity that matches the given expression
    /// </summary>
    /// <param name="expression">The expression to filter the entities.</param>
    /// <param name="CancellationToken">This is a token that can be used to cancel the operation.</param>
    /// <param name="includes">This is an optional parameter that allows you to include related entities in the
    /// query.</param>
    /// <returns>
    /// The first entity that matches the expression.
    /// </returns>  
    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return await IncludeIfNotNull(includes).FirstAsync(expression, cancellationToken);
    }

    /// <summary>
    /// > This function returns the first entity that matches the given expression, or null if no entity matches the
    /// expression
    /// </summary>
    /// <param name="expression">The expression to filter the entities by.</param>
    /// <param name="includes">This is an expression that allows you to include related entities in the query.</param>
    /// <returns>
    /// The first element of a sequence that satisfies a condition or a default value if no such element is found.
    /// </returns>
    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes).FirstOrDefault(expression);
    }

    /// <summary>
    /// > This function returns the first entity that matches the given expression, or null if no entity matches the
    /// expression
    /// </summary>
    /// <param name="expression">The expression to filter the entities by.</param>
    /// <param name="CancellationToken">This is a token that can be used to cancel the operation.</param>
    /// <param name="includes">This is an optional parameter that allows you to include related entities in the
    /// query.</param>
    /// <returns>
    /// The first entity that matches the expression or null if no entity matches the expression.
    /// </returns>
    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return await IncludeIfNotNull(includes).FirstOrDefaultAsync(expression, cancellationToken);
    }
}