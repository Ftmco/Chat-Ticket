using System.Linq.Expressions;

namespace ChTi.Service.Abstraction.Base;

public interface IBaseQuery<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where);

    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> firstOrDefault);

    Task<TEntity> GetAsync(object id);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> any);

    Task<long> CountAsync(Expression<Func<TEntity, bool>> count);

    Task<long> CountAsync();
}