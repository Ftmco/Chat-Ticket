using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Abstraction;

public interface IChatBaseAction : IAsyncDisposable
{
    Task<UpsertChatResponse> UpsertChatAsync(UpsertChatViewModel upsert, IHeaderDictionary headers);

    Task<UpsertChatResponse> CreateAsync(UpsertChatViewModel create, Guid userId);

    Task<UpsertChatResponse> UpdateAsync(UpsertChatViewModel update, Guid userId);
}
