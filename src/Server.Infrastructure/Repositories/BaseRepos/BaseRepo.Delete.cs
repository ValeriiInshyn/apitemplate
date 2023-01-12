#region

using Microsoft.EntityFrameworkCore;
using Server.Contracts.Exceptions;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    public void DeleteNoSave(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        DeleteNoSave(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(object id)
    {
        var entity = await GetByIdAsync(id, CancellationToken.None) ??
                     throw new EntityNotFoundByIdException<TEntity>(id);
        DeleteNoSave(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        DeleteNoSaveRange(entities);
        await SaveChangesAsync();
    }

    public void DeleteNoSaveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }
}