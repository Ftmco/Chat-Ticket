namespace ChTi.Service.Abstraction;

public interface IChatUserAction : IAsyncDisposable
{
    Task<ChatsUsers?> AddUserToChatAsync(AddUserToChatViewModel addUserToChat);

    Task<IEnumerable<ChatsUsers>> AddUserToChatAsync(IEnumerable<AddUserToChatViewModel> addUsersToChats);
}
