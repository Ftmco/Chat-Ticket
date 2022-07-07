using ChTi.Service.Tools.Date;

namespace ChTi.Service.Implemention;

public class MessageViewModelService : IMessageViewModel
{
    readonly IUserGet _userGet;

    readonly IBaseQuery<GroupChat, ChatContext> _chatQuery;

    readonly ITicketViewModel _ticketViewModel;

    readonly IBaseQuery<Message, ChatContext> _messageQuery;

    public MessageViewModelService(IUserGet userGet, IBaseQuery<GroupChat, ChatContext> chatQuery, ITicketViewModel ticketViewModel, IBaseQuery<Message, ChatContext> messageQuery)
    {
        _userGet = userGet;
        _chatQuery = chatQuery;
        _ticketViewModel = ticketViewModel;
        _messageQuery = messageQuery;
    }

    public async Task<IEnumerable<MessageViewModel>> CreateMessageViewModelAsync(IEnumerable<Message> messages, User clientUser)
    {
        List<MessageViewModel> messageViewModels = new();
        foreach (var message in messages)
            messageViewModels.Add(await CreateMessageViewModelAsync(message, clientUser));
        return messageViewModels;
    }

    public async Task<MessageViewModel> CreateMessageViewModelAsync(Message message, User clientUser)
    {
        ChatBase? chat = await _chatQuery.GetAsync(message.ChatId);
        User? user = await _userGet.GetUserByIdAsync(message.UserId);
        MessageViewModel messageViewModel = new(
            Id: message.Id,
            MessageId: message.MessageId,
            ChatToken: chat?.Token ?? "",
            Text: message.Text,
            SendDate: message.CreateDate.ToShamsi(),
            SendTime: message.CreateDate.ToShamsiTime(),
            ReplyMessageId: message.ReplyMessageId,
            ReplyCount: await _messageQuery.CountAsync(m => m.ReplyMessageId == message.MessageId),
            User: await _ticketViewModel.CreateUserViewModelAsync(user));
        messageViewModel.IsSender = clientUser.Id == user?.Id;
        return messageViewModel;
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _userGet.DisposeAsync();
    }
}
