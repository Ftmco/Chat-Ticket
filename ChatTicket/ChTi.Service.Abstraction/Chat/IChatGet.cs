using ChTi.DataBase.Entity;
using ChTi.DataBase.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Abstraction;

public interface IChatGet : IAsyncDisposable
{
    Task<ChatDetailViewModel?> GetChatDetailAsync(string chatToken);

    Task<ChatDetailViewModel?> GetChatDetailAsync(Guid chatId);

    Task<ChatBase?> GetChatAsync(string chatToken);

    Task<ChatBase?> GetChatAsync(Guid chatId);

    Task<IEnumerable<ChatDetailViewModel>> GetUserChatsAsync(IHeaderDictionary headers);

    Task<bool> UserInChatAsync(Guid chatId, Guid userId);
}
