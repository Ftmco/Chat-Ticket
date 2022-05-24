namespace ChTi.Service.Abstraction;

public interface IMessageGet : IAsyncDisposable
{
    Task<long> GetLastMessageIdAsync(Guid chatId);

    Task<IEnumerable<MessageViewModel>> GetMessagesAsync(Guid chatId, long lastMessageId);

    Task<IEnumerable<MessageViewModel>> GetMessagesAsync(string chatToken, long lastMessageId);
}
