namespace ChTi.Service.Abstraction;

public interface IChatViewModel : IAsyncDisposable
{
    Task<ChatDetailViewModel?> CreateChatDetailViewModeAsync(ChatBase? chat);

    Task<IEnumerable<ChatDetailViewModel>> CreateChatDetailViewModeAsync(IEnumerable<ChatBase> chats);

    ChatTypeViewModel GetChatType(ChatBase chat);
}
