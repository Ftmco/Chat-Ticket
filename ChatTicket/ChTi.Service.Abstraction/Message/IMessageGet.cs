using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Abstraction;

public interface IMessageGet : IAsyncDisposable
{
    Task<long> GetLastMessageIdAsync(Guid chatId, ChatType chatType);

    Task<IEnumerable<MessageViewModel>> GetMessagesAsync(Guid chatId, long lastMessageId, IHeaderDictionary headers);

    Task<IEnumerable<MessageViewModel>> GetMessagesAsync(string chatToken, long lastMessageId, IHeaderDictionary headers);

    Task<IEnumerable<MessageViewModel>> GetLastMessagesAsync(string chatToken, int count, ChatType chatType, IHeaderDictionary headers, long? lastMessageId);

    Task<long> MessageCountAsync(Guid chatId, ChatType chatType);
}
