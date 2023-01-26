#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// > Create a new entity, add it to the database, and return the entity
    /// </summary>
    /// <param name="TEntity">The entity type that the repository is for.</param>
    /// <returns>
    /// The entity that was created.
    /// </returns>
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await CreateNoSaveAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// > Adds the given entity to the context without saving it to the database
    /// </summary>
    /// <param name="TEntity">The entity type.</param>
    public async Task CreateNoSaveAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    /// <summary>
    /// It takes a list of entities, adds them to the database, and returns the list of entities
    /// </summary>
    /// <param name="entities">The entities to create.</param>
    /// <returns>
    /// A list of entities.
    /// </returns>
    public virtual async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        var rangeAsync = entities.ToList();
        await DbSet.AddRangeAsync(rangeAsync);
        await SaveChangesAsync();
        return rangeAsync;
    }
}