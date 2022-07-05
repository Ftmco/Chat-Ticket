
using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Implemention;

public class MessageAction : IMessageAction
{
    readonly IUserGet _userGet;

    readonly IBaseCud<Message, ChatContext> _messageCud;

    readonly IChatGet _chatGet;

    readonly IMessageGet _messageGet;

    readonly IMessageViewModel _messageViewModel;

    public MessageAction(IUserGet userGet, IBaseCud<Message, ChatContext> messageCud, IChatGet chatGet, IMessageGet messageGet, IMessageViewModel messageViewModel)
    {
        _userGet = userGet;
        _messageCud = messageCud;
        _chatGet = chatGet;
        _messageGet = messageGet;
        _messageViewModel = messageViewModel;
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _userGet.DisposeAsync();
    }

    public async Task<SendMessageResponse> SendMessageAsync(SendMessageViewModel sendMessage, IHeaderDictionary headers)
    {
        try
        {
            User? user = await _userGet.GetUserBySessionAsync(headers["Auth-Token"].ToString() ?? "");
            if (user == null)
                return new SendMessageResponse(MessageActionStatus.AccessDenied, null);

            ChatBase? chat = await _chatGet.GetChatAsync(sendMessage.ChatToken);
            if (chat == null)
                return new SendMessageResponse(MessageActionStatus.ChatNotFound, null);

            if (await _chatGet.UserInChatAsync(chat.Id, user.Id))
            {
                var lastMessageId = await _messageGet.GetLastMessageIdAsync(chat.Id,ChatType.Group);
                Message message = new()
                {
                    ChatId = chat.Id,
                    CreateDate = DateTime.UtcNow,
                    MessageId = lastMessageId + 1,
                    Text = sendMessage.Text,
                    UserId = user.Id,
                    ReplyMessageId = sendMessage.ReplyMessageId ?? 0
                };
                if (await _messageCud.InsertAsync(message))
                {
                    return new SendMessageResponse(MessageActionStatus.Success, await _messageViewModel.CreateMessageViewModelAsync(message, user));
                }
                return new SendMessageResponse(MessageActionStatus.Exception, null);
            }
            return new SendMessageResponse(MessageActionStatus.AccessDenied, null);
        }
        catch
        {
            return new SendMessageResponse(MessageActionStatus.Exception, null);
        }
    }
}
