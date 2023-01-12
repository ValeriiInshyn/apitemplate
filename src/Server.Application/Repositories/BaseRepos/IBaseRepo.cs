#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Server.Contracts.SubTypes;

#endregion

namespace Server.Application.Repositories.BaseRepos;

public interface IBaseRepo<TEntity> where TEntity : class
{
    #region Paginate

    ( IQueryable<TEntity> Collection, int TotalCount) Paginate(int skipItems, int takeItems, string orderBy,
        OrderDirection orderDirection,
        Expression<Func<TEntity, bool>>? expression);

    #endregion

    #region Basic

    void Attach(TEntity entity);

    Task<TEntity> CreateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);

    Task DeleteAsync(object id);
    Task<bool> ExistsAsync(object id, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

    TEntity First(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);


    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);


    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);


    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection);

    Task<TEntity?> GetByIdAsync(object id, CancellationToken token);

    IQueryable<TEntity> IncludeIfNotNull(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    Task<bool> IsEmptyAsync(CancellationToken cancellationToken);

    Task<bool> IsEmptyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

    Task SaveChangesAsync();

    Task UpdateAsync(TEntity entity);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, string orderBy,
        OrderDirection orderDirection);

    #endregion


    #region BasicNoSave

    Task CreateNoSaveAsync(TEntity entity);

    void DeleteNoSave(TEntity entity);

    void UpdateNoSave(TEntity entity);

    #endregion


    #region BasicRange

    void AttachRange(IEnumerable<TEntity> entities);

    Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);

    Task DeleteRangeAsync(IEnumerable<TEntity> entities);

    void DeleteNoSaveRange(IEnumerable<TEntity> entities);

    Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    void UpdateRangeNoSave(IEnumerable<TEntity> entities);

    #endregion
}