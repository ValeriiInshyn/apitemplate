#region

using Microsoft.EntityFrameworkCore;
using Server.Contracts.Exceptions;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// > This function removes the entity from the database without saving the changes
    /// </summary>
    /// <param name="TEntity">The entity type that the repository is for.</param>
    public void DeleteNoSave(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    /// <summary>
    /// > Delete the entity and save the changes
    /// </summary>
    /// <param name="TEntity">The entity type.</param>
    public async Task DeleteAsync(TEntity entity)
    {
        DeleteNoSave(entity);
        await SaveChangesAsync();
    }

    /// <summary>
    /// > Delete the entity with the given id, or throw an exception if it doesn't exist
    /// </summary>
    /// <param name="id">The id of the entity to delete.</param>
    public async Task DeleteAsync(object id)
    {
        var entity = await GetByIdAsync(id, CancellationToken.None) ??
                     throw new EntityNotFoundByIdException<TEntity>(id);
        DeleteNoSave(entity);
        await SaveChangesAsync();
    }

    /// <summary>
    /// > Delete a range of entities from the database and save the changes
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        DeleteNoSaveRange(entities);
        await SaveChangesAsync();
    }

    /// <summary>
    /// > This function deletes a range of entities from the database without saving the changes
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    public void DeleteNoSaveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }
}