using MongoDB.Driver;
using System.Linq.Expressions;

namespace ChTi.Service.Abstraction.Base;

public interface IBaseCud<TEntity> where TEntity : class
{
    Task<bool> InsertAsync(TEntity entity);

    Task<bool> InsertAsync(IEnumerable<TEntity> entities);

    Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> update);
}
