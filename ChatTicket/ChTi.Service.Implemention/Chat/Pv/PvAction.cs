namespace ChTi.Service.Implemention;

public class PvAction : IPvAction
{
    readonly IUserGet _userGet;

    readonly IBaseQuery<PvChat, ChatContext> _pvChatQuery;

    readonly IBaseCud<PvChat, ChatContext> _pvChatCud;

    readonly IChatViewModel _chatViewModel;

    public PvAction(IUserGet userGet, IBaseQuery<PvChat, ChatContext> pvChatQuery,
        IBaseCud<PvChat, ChatContext> pvChatCud, IChatViewModel chatViewModel)
    {
        _userGet = userGet;
        _pvChatQuery = pvChatQuery;
        _pvChatCud = pvChatCud;
        _chatViewModel = chatViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<PvChatResponse> StartPvChatAsync(IHeaderDictionary headers, Guid userId)
    {
        var user = await _userGet.GetUserBySessionAsync(headers["Auth-Token"].ToString() ?? "");
        var oppsiteUser = await _userGet.GetUserByIdAsync(userId);

        if (user == null)
            return new PvChatResponse(ChatActionStatus.UserNotAuthorized, null);
        if (oppsiteUser == null)
            return new PvChatResponse(ChatActionStatus.UserNotFound, null);

        if (user.Id == oppsiteUser.Id)
            return new PvChatResponse(ChatActionStatus.Illegal,null);

        PvChat? pvChat = await _pvChatQuery.GetAsync(pv => pv.StarterUserId == user.Id && pv.OppsiteUserId == oppsiteUser.Id);

        if (pvChat == null)
        {
            pvChat = new()
            {
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                StarterUserId = user.Id,
                OppsiteUserId = oppsiteUser.Id,
                Status = (short)ChatStatus.Active,
                Token = $"PV-{60.CreateToken()}"
            };
            if (!await _pvChatCud.InsertAsync(pvChat))
                return new PvChatResponse(ChatActionStatus.Exception, null);
        }

        return new PvChatResponse(ChatActionStatus.Success, await _chatViewModel.CreatePvChatDetailViewModelAsync(user, oppsiteUser, pvChat));
    }

    #region -- Non Actions --

    public Task<UpsertChatResponse> UpdateAsync(UpsertChatViewModel update, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<UpsertChatResponse> UpsertChatAsync(UpsertChatViewModel upsert, IHeaderDictionary headers)
    {
        throw new NotImplementedException();
    }

    public Task<UpsertChatResponse> CreateAsync(UpsertChatViewModel create, Guid userId)
    {
        throw new NotImplementedException();
    }

    #endregion
}
