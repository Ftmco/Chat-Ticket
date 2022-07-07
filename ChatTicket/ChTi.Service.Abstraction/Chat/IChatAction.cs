using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Abstraction;

public interface IChatAction : IAsyncDisposable
{
    Task<UpsertChatResponse> UpsertChatAsync(UpsertChatViewModel upsert, IHeaderDictionary headers);

    Task<UpsertChatResponse> CreateAsync(UpsertChatViewModel create, Guid userId);

    Task<UpsertChatResponse> UpdateAsync(UpsertChatViewModel update, Guid userId);

    Task<ChatsUsers?> AddUserToChatAsync(Guid chatId, Guid userId, ChatUserType userType);

    Task<PvChatResponse> StartPvChatAsync(IHeaderDictionary headers, Guid userId);
}

public interface IChatBaseAction : IAsyncDisposable
{

}
