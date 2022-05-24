using ChTi.Service.Tools.Date;

namespace ChTi.Service.Implemention;

public class ChatViewModelService : IChatViewModel
{
    readonly IMessageGet _messageGet;


    public ChatViewModelService(IMessageGet messageGet)
    {
        _messageGet = messageGet;
    }

    public ChatTypeViewModel GetChatType(Chat chat)
            => new(
             Type: (ChatType)chat.Type switch
             {
                 ChatType.Group => "گروه",
                 ChatType.Pv => "خصوصی",
                 _ => "گروه",
             },
             Code: chat.Type);

    public async Task<ChatDetailViewModel?> CreateChatDetailViewModeAsync(Chat? chat)
    {
        if (chat == null)
            return default;

        ChatDetailViewModel chatDetail = new(
            Id: chat.Id, Token: chat.Token,
            Name: chat.Name, Description: chat.Description,
            CreateDate: chat.CreateDate.ToShamsi(),
            UpdateDate: chat.UpdateDate.ToShamsi(),
            LastMessageId: await _messageGet.GetLastMessageIdAsync(chat.Id),
            GetChatType(chat), null);

        return chatDetail;
    }

    public async Task<IEnumerable<ChatDetailViewModel>> CreateChatDetailViewModeAsync(IEnumerable<Chat> chats)
    {
        List<ChatDetailViewModel> chatDetailViewModels = new();
        foreach (var chat in chats)
        {
            ChatDetailViewModel? viewModel = await CreateChatDetailViewModeAsync(chat);
            if (viewModel != null)
                chatDetailViewModels.Add(viewModel);
        }
        return chatDetailViewModels;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
