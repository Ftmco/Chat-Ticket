using ChTi.DataBase.Entity;
using ChTi.DataBase.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Abstraction;

public interface IChatGet : IAsyncDisposable
{
    Task<ChatDetailViewModel?> GetChatDetailAsync(string chatToken);

    Task<ChatDetailViewModel?> GetChatDetailAsync(Guid chatId);

    Task<GroupChat?> GetChatAsync(string chatToken);

    Task<GroupChat?> GetChatAsync(Guid chatId);

    Task<IEnumerable<ChatDetailViewModel>> GetUserChatsAsync(IHeaderDictionary headers);

    Task<IEnumerable<PvChatDetailViewModel>> GetUserPvChatsAsync(IHeaderDictionary headers);

    Task<IEnumerable<ChatDetailViewModel>> GetUserGroupsAsync(IHeaderDictionary headers);

    Task<IEnumerable<ChatDetailViewModel>> GetUserChannelsAsync(IHeaderDictionary headers);

    Task<bool> UserInChatAsync(Guid chatId, Guid userId);
}

public interface IChatBaseGet<TChat> : IAsyncDisposable
{
    Task<bool> UserInChatAsync(Guid chatId, Guid userId);

    Task<TChat?> GetChatAsync(string chatToken);

    Task<TChat?> GetChatAsync(Guid chatId);
}
