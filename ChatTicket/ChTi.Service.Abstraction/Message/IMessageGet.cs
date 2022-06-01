using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Abstraction;

public interface IMessageGet : IAsyncDisposable
{
    Task<long> GetLastMessageIdAsync(Guid chatId);

    Task<IEnumerable<MessageViewModel>> GetMessagesAsync(Guid chatId, long lastMessageId, IHeaderDictionary headers);

    Task<IEnumerable<MessageViewModel>> GetMessagesAsync(string chatToken, long lastMessageId, IHeaderDictionary headers);

    Task<IEnumerable<MessageViewModel>> GetLastMessagesAsync(string chatToken, int count, IHeaderDictionary headers, long? lastMessageId);

    Task<long> MessageCountAsync(Guid chatId);
}
