namespace ChTi.Service.Abstraction;

public interface IChatViewModel : IAsyncDisposable
{
    Task<ChatDetailViewModel?> CreateChatDetailViewModeAsync(Chat? chat);

    Task<IEnumerable<ChatDetailViewModel>> CreateChatDetailViewModeAsync(IEnumerable<Chat> chats);

    ChatTypeViewModel GetChatType(Chat chat);
}
