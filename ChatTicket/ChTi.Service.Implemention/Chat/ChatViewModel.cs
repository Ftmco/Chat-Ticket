using ChTi.Service.Tools.Date;

namespace ChTi.Service.Implemention;

public class ChatViewModelService : IChatViewModel
{
    readonly IMessageGet _messageGet;

    readonly IUserGet _userGet;

    public ChatViewModelService(IMessageGet messageGet, IUserGet userGet)
    {
        _messageGet = messageGet;
        _userGet = userGet;
    }

    public ChatTypeViewModel GetChatType(GroupChat chat)
            => new(
             Type: (ChatType)chat.Type switch
             {
                 ChatType.Group => "گروه",
                 ChatType.Pv => "خصوصی",
                 _ => "گروه",
             },
             Code: chat.Type);

    public async Task<ChatDetailViewModel?> CreateChatDetailViewModeAsync(GroupChat? chat)
    {
        if (chat == null)
            return default;

        ChatDetailViewModel chatDetail = new(
            Id: chat.Id, Token: chat.Token,
            Name: chat.Name, Description: chat.Description,
            CreateDate: chat.CreateDate.ToShamsi(),
            UpdateDate: chat.UpdateDate.ToShamsi(),
            LastMessageId: await _messageGet.GetLastMessageIdAsync(chat.Id, ChatType.Group),
            MessageCount: await _messageGet.MessageCountAsync(chat.Id, ChatType.Group),
            GetChatType(chat), null);

        return chatDetail;
    }

    public async Task<IEnumerable<ChatDetailViewModel>> CreateChatDetailViewModeAsync(IEnumerable<GroupChat> chats)
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

    public async Task<PvChatDetailViewModel> CreatePvChatDetailViewModelAsync(User currentUser, User oppsiteUser, PvChat? chat)
    {
        if (chat == null)
            return default;

        IEnumerable<Avatar>? userProfile = await _userGet.GetUserAvatarsAsync(oppsiteUser.Id, false);

        PvChatDetailViewModel pvChat = new(
            Id: chat.Id,
            Token: chat.Token, Name: oppsiteUser.FullName, MobileNo: oppsiteUser.MobileNo, Email: oppsiteUser.Email,
            CreateDate: chat.CreateDate.ToShamsi(), UpdateDate: chat.UpdateDate.ToShamsi(),
            LastMessageId: await _messageGet.GetLastMessageIdAsync(chat.Id, ChatType.Pv),
            MessageCount: await _messageGet.MessageCountAsync(chat.Id, ChatType.Pv),
            Avatars: userProfile.Select(up => new FileViewModel(up.FileId, up.FileToken)));

        return pvChat;
    }

    public async Task<IEnumerable<PvChatDetailViewModel>> CreatePvChatDetailViewModelAsync(User currentUser, IEnumerable<PvChat>? chats)
    {
        List<PvChatDetailViewModel> result = new();
        foreach (var chat in chats)
        {
            var oppsiteUser = await _userGet.GetUserByIdAsync(chat.OppsiteUserId);
            result.Add(await CreatePvChatDetailViewModelAsync(currentUser, oppsiteUser, chat));
        }
        return result;
    }

    public async Task<IEnumerable<PvChatDetailViewModel>> CreatePvChatDetailViewModelAsync(User currentUser, User oppsiteUser, IEnumerable<PvChat>? chats)
    {
        List<PvChatDetailViewModel> result = new();
        foreach (var chat in chats)
            result.Add(await CreatePvChatDetailViewModelAsync(currentUser, oppsiteUser, chat));

        return result;
    }
}
