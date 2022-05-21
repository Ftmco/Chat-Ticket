using ChTi.Service.Tools.Date;

namespace ChTi.Service.Implemention;

public class ChatViewModelService : IChatViewModel
{
    public ChatTypeViewModel GetChatType(Chat chat)
            => new(
             Type: (ChatType)chat.Type switch
             {
                 ChatType.Group => "گروه",
                 ChatType.Pv => "خصوصی",
                 _ => "گروه",
             },
             Code: chat.Type);

    public Task<ChatDetailViewModel?> CreateChatDetailViewModeAsync(Chat? chat)
    {
        ChatDetailViewModel chatDetail = new(
            Id: chat.Id, Token: chat.Token,
            Name: chat.Name, Bio: chat.Description,
            CreateDate: chat.CreateDate.ToShamsi(),
            0, GetChatType(chat), null);

        return Task.FromResult(chatDetail);
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
