using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Core;

namespace ChTi.Service.Abstraction.Base;

public interface IBaseCud<TEntity> where TEntity : class
{
    Task<bool> InsertAsync(TEntity entity);

    Task<bool> InsertAsync(IEnumerable<TEntity> entities);

    Task<bool> UpdateAsync(Expression<Func<TEntity,bool>> filter,UpdateDefinition<TEntity> update);
}
