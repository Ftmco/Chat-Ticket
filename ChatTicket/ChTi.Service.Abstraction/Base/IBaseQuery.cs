using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Abstraction.Base;

public interface IBaseQuery<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> where);

    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> firstOrDefault);

    Task<TEntity> GetAsync(object id);
}