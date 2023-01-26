#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Server.Application.Repositories.BaseRepos;
using Server.Contracts.SubTypes;
using Server.Infrastructure.Repositories.Extensions;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

/* It's a base class for repositories that provides basic CRUD operations */
public abstract partial class BaseRepo<TContext, TEntity> : IBaseRepo<TEntity>
    where TEntity : class where TContext : DbContext
{
  
    /* It's a private field that is used to access the context. */
    private readonly TContext _context;
    /* It's a property that is used to access the table. */
    protected readonly DbSet<TEntity> DbSet;

    /* It's a constructor that takes a context and a default order by expression. */
    protected BaseRepo(TContext context, Expression<Func<TEntity, object>> defaultOrderBy)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }


    /// <summary>
    /// GetAll() returns all the rows in the table.
    /// </summary>
    /// <returns>
    /// IQueryable<TEntity>
    /// </returns>
    public IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    /// <summary>
    /// Get all entities from the database, ordered by the specified property in the specified direction.
    /// </summary>
    /// <param name="orderBy">The name of the property to order by.</param>
    /// <param name="OrderDirection">An enum with the values Ascending and Descending</param>
    /// <summary>
    /// > Finds an entity with the given primary key values. If an entity with the given primary key values exists in the
    /// context, then it is returned immediately without making a request to the store. Otherwise, a request is made to the
    /// store for an entity with the given primary key values and this entity, if found, is attached to the context and
    /// returned. If no entity is found in the context or the store, then null is returned
    /// </summary>
    /// <param name="id">The id of the entity to find.</param>
    /// <param name="CancellationToken">This is a token that can be used to cancel the operation.</param>
    /// <returns>
    /// The entity with the given id.
    /// </returns>
    /// <returns>
    /// IQueryable<TEntity>
    /// <summary>
    /// It saves the changes to the database
    /// </summary>
    /// </returns>
    public IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection)
    {
        return DbSet.OrderByWithDirection(orderBy, orderDirection);
    }

    /// <summary>
    /// > Finds an entity with the given primary key values. If an entity with the given primary key values exists in the
    /// context, then it is returned immediately without making a request to the store. Otherwise, a request is made to the
    /// store for an entity with the given primary key values and this entity, if found, is attached to the context and
    /// returned. If no entity is found in the context or the store, then null is returned
    /// </summary>
    /// <param name="id">The id of the entity to get.</param>
    /// <param name="CancellationToken">This is a token that can be used to cancel the operation.</param>
    /// <returns>
    /// The entity with the given id.
    /// </returns>
    public async Task<TEntity?> GetByIdAsync(object id, CancellationToken token)
    {
        return await DbSet.FindAsync(new[] { id }, token);
    }

    /// <summary>
    /// It saves the changes to the database
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// If the includes parameter is null, return the DbSet, otherwise return the result of the includes function
    /// </summary>
    /// <param name="includes">A function that takes an IQueryable<TEntity> and returns an IIncludableQueryable<TEntity,
    /// object>.</param>
    /// <returns>
    /// IQueryable<TEntity>
    /// </returns>
    public IQueryable<TEntity> IncludeIfNotNull(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        if (includes is null)
            return DbSet;
        return includes(DbSet);
    }
}