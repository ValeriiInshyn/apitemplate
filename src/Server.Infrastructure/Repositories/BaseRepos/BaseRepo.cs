#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Server.Application.Repositories.BaseRepos;
using Server.Contracts.SubTypes;
using Server.Infrastructure.Repositories.Extensions;

#endregion

namespace Server.Infrastructure.Repositories.BaseRepos;

public abstract partial class BaseRepo<TContext, TEntity> : IBaseRepo<TEntity>
    where TEntity : class where TContext : DbContext
{
    private readonly TContext _context;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepo(TContext context, Expression<Func<TEntity, object>> defaultOrderBy)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }


    public IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    public IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection)
    {
        return DbSet.OrderByWithDirection(orderBy, orderDirection);
    }

    public async Task<TEntity?> GetByIdAsync(object id, CancellationToken token)
    {
        return await DbSet.FindAsync(new[] { id }, token);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IQueryable<TEntity> IncludeIfNotNull(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        if (includes is null)
            return DbSet;
        return includes(DbSet);
    }
}