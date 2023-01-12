#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    public void Attach(TEntity entity)
    {
        DbSet.Attach(entity);
    }

    public void AttachRange(IEnumerable<TEntity> entities)
    {
        DbSet.AttachRange(entities);
    }
}