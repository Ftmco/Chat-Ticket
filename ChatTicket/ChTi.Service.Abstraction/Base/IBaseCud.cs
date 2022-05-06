using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Core;

namespace ChTi.Service.Abstraction.Base;

public interface IBaseCud<TEntity> where TEntity : class
{
    Task<bool> InsertAsync(TEntity entity);

    Task<bool> InsertAsync(IEnumerable<TEntity> entities);
}
