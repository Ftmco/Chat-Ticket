namespace ChTi.Service.Implemention;

public class ChatGet : IChatGet
{
    readonly IChatViewModel _chatViewModel;

    readonly IBaseQuery<Chat> _chatQuery;

    public ChatGet(IChatViewModel chatViewModel, IBaseQuery<Chat> chatQuery)
    {
        _chatViewModel = chatViewModel;
        _chatQuery = chatQuery;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<Chat?> GetChatAsync(string chatToken)
        => await _chatQuery.GetAsync(c => c.Token == chatToken);

    public async Task<Chat?> GetChatAsync(Guid chatId)
        => await _chatQuery.GetAsync(chatId);

    public async Task<ChatDetailViewModel?> GetChatDetailAsync(string chatToken)
    {
        Chat? chat = await GetChatAsync(chatToken);
        return await _chatViewModel.CreateChatDetailViewModeAsync(chat);
    }

    public async Task<ChatDetailViewModel?> GetChatDetailAsync(Guid chatId)
    {
        Chat? chat = await GetChatAsync(chatId);
        return await _chatViewModel.CreateChatDetailViewModeAsync(chat);
    }
}
