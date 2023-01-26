#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public partial class BaseRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    /// > Attach() is used to attach an entity to the context
    /// </summary>
    /// <param name="TEntity">The entity type.</param>
    public void Attach(TEntity entity)
    {
        DbSet.Attach(entity);
    }

    /// <summary>
    /// AttachRange() attaches a collection of entities to the context
    /// </summary>
    /// <param name="entities">The entities to attach.</param>
    public void AttachRange(IEnumerable<TEntity> entities)
    {
        DbSet.AttachRange(entities);
    }
}