using ChTi.DataBase.Entity;
using ChTi.DataBase.ViewModel;

namespace ChTi.Service.Abstraction;

public interface IChatGet : IAsyncDisposable
{
    Task<ChatDetailViewModel?> GetChatDetailAsync(string chatToken);

    Task<ChatDetailViewModel?> GetChatDetailAsync(Guid chatId);

    Task<Chat?> GetChatAsync(string chatToken);

    Task<Chat?> GetChatAsync(Guid chatId);
}
