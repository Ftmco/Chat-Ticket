using MongoDB.Driver;
using System.Linq.Expressions;

namespace ChTi.Service.Implemention.Base;

public class BaseCud<TEntity> : IBaseCud<TEntity> where TEntity : class
{
    readonly IMongoClient _client;

    readonly IMongoDatabase _database;

    readonly IMongoCollection<TEntity> _collection;

    public BaseCud(IMongoClient client)
    {
        _client = client;
        _database = _client.GetDatabase("ChTi_DB");
        _collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public async Task<bool> InsertAsync(TEntity entity)
    {
        try
        {
            await _collection.InsertOneAsync(entity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> InsertAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            await _collection.InsertManyAsync(entities);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> update)
    {
        try
        {
            await _collection.UpdateOneAsync(filter, update);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
