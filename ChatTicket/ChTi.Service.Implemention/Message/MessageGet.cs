using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Implemention;

public class MessageGet : IMessageGet
{
    readonly IBaseQuery<Message, ChatContext> _messageQuery;

    readonly IBaseQuery<ChatBase, ChatContext> _chatQuery;

    readonly IMessageViewModel _messageViewModel;

    readonly IUserGet _userGet;

    public MessageGet(IBaseQuery<Message, ChatContext> messageQuery, IMessageViewModel messageViewModel, IBaseQuery<ChatBase, ChatContext> chatQuery, IUserGet userGet)
    {
        _messageQuery = messageQuery;
        _messageViewModel = messageViewModel;
        _chatQuery = chatQuery;
        _userGet = userGet;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<long> GetLastMessageIdAsync(Guid chatId, ChatType chatType)
    {
        Message? message = await _messageQuery.MaxAsync(m => m.ChatId == chatId, m => m.MessageId);
        return message == null ? 0 : message.MessageId;
    }

    public async Task<IEnumerable<MessageViewModel>> GetLastMessagesAsync(string chatToken, int count, ChatType chatType, IHeaderDictionary headers, long? lastMessageId)
    {
        var chat = await _chatQuery.GetAsync(c => c.Token == chatToken);
        if (chat != null)
        {
            var user = await _userGet.GetUserBySessionAsync(headers["Auth-Token"].ToString() ?? "");
            var messageCount = await MessageCountAsync(chat.Id,chatType);
            var messages = await _messageQuery.GetAllAsync(m => m.ChatId == chat.Id, (int)(messageCount - count), count);
            return user != null ?
                await _messageViewModel.CreateMessageViewModelAsync(messages, user) :
              new List<MessageViewModel>();
        }
        return new List<MessageViewModel>();
    }

    public async Task<IEnumerable<MessageViewModel>> GetMessagesAsync(Guid chatId, long lastMessageId, IHeaderDictionary headers)
    {
        var user = await _userGet.GetUserBySessionAsync(headers["Auth-Token"].ToString() ?? "");
        IEnumerable<Message>? messages = await _messageQuery.GetAllAsync(m => m.ChatId == chatId && m.MessageId > lastMessageId);
        return user != null ?
            await _messageViewModel.CreateMessageViewModelAsync(messages, user) :
                new List<MessageViewModel>();
    }

    public async Task<IEnumerable<MessageViewModel>> GetMessagesAsync(string chatToken, long lastMessageId, IHeaderDictionary headers)
    {
        var chat = await _chatQuery.GetAsync(c => c.Token == chatToken);
        if (chat == null)
            return new List<MessageViewModel>();
        return await GetMessagesAsync(chat.Id, lastMessageId, headers);
    }

    public async Task<long> MessageCountAsync(Guid chatId,ChatType chatType)
            => await _messageQuery.CountAsync(m => m.ChatId == chatId);
}
