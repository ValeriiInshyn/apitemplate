#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// > Update the entity and save the changes to the database
    /// </summary>
    /// <param name="TEntity">The entity type.</param>
    public async Task UpdateAsync(TEntity entity)
    {
        UpdateNoSave(entity);
        await SaveChangesAsync();
    }

    /// <summary>
    /// > UpdateNoSave() is a function that updates an entity without saving the changes to the database
    /// </summary>
    /// <param name="TEntity">The entity type that the repository is for.</param>
    public void UpdateNoSave(TEntity entity)
    {
        DbSet.Update(entity);
    }

    /// <summary>
    /// > UpdateRangeAsync() is a function that updates a range of entities and saves the changes asynchronously
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        UpdateRangeNoSave(entities);
        await SaveChangesAsync();
    }

    /// <summary>
    /// > UpdateRangeNoSave() is a function that updates a range of entities without saving them to the database
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    public void UpdateRangeNoSave(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
    }
}