using Identity.Client.Models;

namespace ChTi.Service.Abstraction;

public interface IChatViewModel : IAsyncDisposable
{
    Task<ChatDetailViewModel?> CreateChatDetailViewModeAsync(GroupChat? chat);

    Task<IEnumerable<ChatDetailViewModel>> CreateChatDetailViewModeAsync(IEnumerable<GroupChat> chats);

    Task<PvChatDetailViewModel> CreatePvChatDetailViewModelAsync(User currentUser, User oppsiteUser, PvChat? chat);

    Task<IEnumerable<PvChatDetailViewModel>> CreatePvChatDetailViewModelAsync(User currentUser, IEnumerable<PvChat>? chats);

    Task<IEnumerable<PvChatDetailViewModel>> CreatePvChatDetailViewModelAsync(User currentUser,User oppsiteUser, IEnumerable<PvChat>? chats);
    
    ChatTypeViewModel GetChatType(GroupChat chat);
}
