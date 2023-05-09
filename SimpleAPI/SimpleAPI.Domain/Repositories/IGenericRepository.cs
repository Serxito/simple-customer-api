using SimpleAPI.Domain.Entities;

namespace SimpleAPI.Domain.Repositories;

/// <summary>
/// Generic repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAllAsync(FilterSettings settings);

    /// <summary>
    /// Get specific entity by identifier
    /// </summary>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(Guid Id);

    /// <summary>
    /// Crete entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> CreateAsync(TEntity entity);
    
    /// <summary>
    /// Update entity
    /// </summary>
    /// <returns></returns>
    Task<TEntity> UpdateAsync(TEntity entity);
    
    /// <summary>
    /// Delete entity
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAsync(Guid Id);
}