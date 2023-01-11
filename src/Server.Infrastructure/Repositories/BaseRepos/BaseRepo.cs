#region @copyright by IntenseLab 2022

// // // /////////////////////////////////////////////////////////////////////////////////////
// // // Product name: B2C-QuoteMedia-Data-Services
// // // Product short name: B2C-QM-Admin
// // // Vendor: IntenseLab LLC
// // // License: IntenseLab License
// // // Vendor mail: info@intenselab.com
// // //
// // // Product version: v1.0.1.100
// // // Product description: www.intenselab.com/go/en/solutions
// // // /////////////////////////////////////////////////////////////////////////////////////

#endregion

using Microsoft.EntityFrameworkCore;
using Server.Application.Repositories.BaseRepos;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Server.Contracts.Exceptions;

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

    public async Task<TEntity?> GetByIdAsync(object id, CancellationToken token)
    {
        return await DbSet.FindAsync(new[] { id }, token);
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await CreateNoSaveAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(object id)
    {
        TEntity entity = await GetByIdAsync(id, CancellationToken.None) ??
                         throw new EntityNotFoundByIdException<TEntity>(id);
        DeleteNoSave(entity);
        await SaveChangesAsync();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Attach(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task CreateNoSaveAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void DeleteNoSave(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public void DeleteNoSave(object id)
    {
        throw new NotImplementedException();
    }

    public void UpdateNoSave(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public void UpdateRangeNoSave(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public void AttachRange(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public void DeleteNoSaveRange(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }
}