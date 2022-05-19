using StackExchange.Redis;

namespace ChTi.Client.Cache;

public interface ICache : IAsyncDisposable
{
    Task<TModel?> GetItemAsync<TModel>(string key);

    Task<TModel?> SetItemAsync<TModel>(string key, TModel obj);

    Task<ConnectionMultiplexer> ConnectAsync();
}
