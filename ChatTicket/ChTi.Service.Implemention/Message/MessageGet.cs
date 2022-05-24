namespace ChTi.Service.Implemention;

public class MessageGet : IMessageGet
{
    readonly IBaseQuery<Message, ChatContext> _messageQuery;

    readonly IBaseQuery<Chat, ChatContext> _chatQuery;

    readonly IMessageViewModel _messageViewModel;

    public MessageGet(IBaseQuery<Message, ChatContext> messageQuery,IMessageViewModel messageViewModel, IBaseQuery<Chat, ChatContext> chatQuery)
    {
        _messageQuery = messageQuery;
        _messageViewModel = messageViewModel;
        _chatQuery = chatQuery; 
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<long> GetLastMessageIdAsync(Guid chatId)
    {
        Message? message = await _messageQuery.MaxAsync(m => m.ChatId == chatId, m => m.MessageId);
        return message == null ? 0 : message.MessageId;
    }

    public async Task<IEnumerable<MessageViewModel>> GetMessagesAsync(Guid chatId, long lastMessageId)
    {
        IEnumerable<Message>? messages = await _messageQuery.GetAllAsync(m => m.ChatId == chatId && m.MessageId > lastMessageId);
        return await _messageViewModel.CreateMessageViewModelAsync(messages);
    }

    public async Task<IEnumerable<MessageViewModel>> GetMessagesAsync(string chatToken, long lastMessageId)
    {
        var chat = await _chatQuery.GetAsync(c => c.Token == chatToken);
        if(chat == null)
            return new List<MessageViewModel>();
        return await GetMessagesAsync(chat.Id, lastMessageId);
    }
}
