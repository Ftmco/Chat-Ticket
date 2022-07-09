namespace ChTi.Service.Implemention;

public class PvGet : IPvGet
{
    readonly IUserGet _userGet;

    readonly IBaseQuery<PvChat, ChatContext> _pvChatQuery;

    readonly IChatViewModel _chatViewModel;

    public PvGet(IUserGet userGet, IBaseQuery<PvChat, ChatContext> pvChatQuery, IChatViewModel chatViewModel)
    {
        _userGet = userGet;
        _pvChatQuery = pvChatQuery;
        _chatViewModel = chatViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public Task<PvChat?> GetChatAsync(string chatToken)
    {
        throw new NotImplementedException();
    }

    public Task<PvChat?> GetChatAsync(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<PvChatResponse?> GetChatDetailAsync(string chatToken)
    {
        throw new NotImplementedException();
    }

    public Task<PvChatResponse?> GetChatDetailAsync(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PvChatDetailViewModel>> GetUserPvChatsAsync(IHeaderDictionary headers)
    {
        User? user = await _userGet.GetUserBySessionAsync(headers["Auth-Token"].ToString() ?? "");
        if (user == null)
            return new List<PvChatDetailViewModel>();

        IEnumerable<PvChat>? chats = await _pvChatQuery.GetAllAsync(pv => pv.StarterUserId == user.Id || pv.OppsiteUserId == user.Id);
        return await _chatViewModel.CreatePvChatDetailViewModelAsync(user, chats);
    }

    public async Task<bool> UserInChatAsync(Guid chatId, Guid userId)
        => await _pvChatQuery.AnyAsync(pv => pv.Id == chatId && (pv.StarterUserId == userId || pv.OppsiteUserId == userId));
}
