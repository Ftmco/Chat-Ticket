using ChTi.Service.Abstraction.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention.Base;

public class BaseQuery<TEntity> : IBaseQuery<TEntity> where TEntity : class
{
    readonly IMongoClient _client;

    readonly IMongoDatabase _database;

    readonly IMongoCollection<TEntity> _collection;

    public BaseQuery(IMongoClient client)
    {
        _client = client;
        _database = _client.GetDatabase("ChTi_DB");
        _collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> any)
    {
        IEnumerable<TEntity>? all = await GetAllAsync(any);
        return all.Any();
    }

    public async Task<long> CountAsync(Expression<Func<TEntity, bool>> count)
            => await _collection.CountDocumentsAsync(count);

    public async Task<long> CountAsync()
        => await _collection.CountDocumentsAsync(new BsonDocument());

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        IAsyncCursor<TEntity>? entities = await _collection.FindAsync(new BsonDocument());
        return entities.ToEnumerable();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where)
    {
        IAsyncCursor<TEntity>? entities = await _collection.FindAsync(where);
        return entities.ToEnumerable();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> firstOrDefault)
    {
        return await _collection.Find(firstOrDefault).SingleOrDefaultAsync();
    }

    public async Task<TEntity> GetAsync(object id)
    {
        var find = await _collection.FindAsync(new BsonDocument("_id", BsonValue.Create(id)));
        return await find.SingleOrDefaultAsync();
    }
}