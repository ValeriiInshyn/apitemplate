#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    public async Task UpdateAsync(TEntity entity)
    {
        UpdateNoSave(entity);
        await SaveChangesAsync();
    }

    public void UpdateNoSave(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        UpdateRangeNoSave(entities);
        await SaveChangesAsync();
    }

    public void UpdateRangeNoSave(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
    }
}