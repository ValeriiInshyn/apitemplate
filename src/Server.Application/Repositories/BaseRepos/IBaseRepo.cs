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

namespace Server.Application.Repositories.BaseRepos;

public partial interface IBaseRepo<TEntity> where TEntity : class
{
    #region Basic
    
    IQueryable<TEntity> GetAll();
    
    //IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection);
    
    Task<TEntity?> GetByIdAsync(object id, CancellationToken token);

    Task<TEntity> CreateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);

    Task DeleteAsync(object id);

    Task UpdateAsync(TEntity entity);
    
    void Attach(TEntity entity);

    Task SaveChangesAsync();
    
    #endregion

    
    #region BasicNoSave

    Task CreateNoSaveAsync(TEntity entity);

    void DeleteNoSave(TEntity entity);

    void DeleteNoSave(object id);

    void UpdateNoSave(TEntity entity);

    #endregion

    
    #region BasicRange

    Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);
    
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    
    void UpdateRangeNoSave(IEnumerable<TEntity> entities);
    
    void AttachRange(IEnumerable<TEntity> entities);
    
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    
    void DeleteNoSaveRange(IEnumerable<TEntity> entities);
    
    #endregion
    
}