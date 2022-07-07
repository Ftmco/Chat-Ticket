using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class GroupAction : IGroupAction
{
    readonly IUserGet _userGet;

    readonly IChatViewModel _chatViewModel;

    readonly IChatUserAction _chatUserAction;

    readonly IBaseCud<GroupChat, ChatContext> _groupChatCud;

    readonly IBaseQuery<ChatsUsers, ChatContext> _chatsUsersQuery;

    readonly IGroupGet _groupGet;

    public GroupAction(IUserGet userGet, IChatViewModel chatViewModel,
        IBaseCud<GroupChat, ChatContext> groupChatCud, IBaseQuery<ChatsUsers, ChatContext> chatsUsersQuery,
        IGroupGet groupGet, IChatUserAction chatUserAction)
    {
        _userGet = userGet;
        _chatViewModel = chatViewModel; 
        _chatsUsersQuery = chatsUsersQuery;
        _chatUserAction = chatUserAction;
        _groupChatCud = groupChatCud;
        _groupGet = groupGet;
    }

    public async Task<UpsertChatResponse> CreateAsync(UpsertChatViewModel create, Guid userId)
    {
        GroupChat chat = new()
        {
            CreateDate = DateTime.UtcNow,
            Description = create.Description,
            Name = create.Name,
            Status = (short)ChatStatus.Active,
            Token = 60.CreateToken(),
            Type = (short)create.Type,
            UpdateDate = DateTime.UtcNow
        };
        if (await _groupChatCud.InsertAsync(chat))
        {
            await _chatUserAction.AddUserToChatAsync(new AddUserToChatViewModel(chat.Id, userId, ChatUserType.Owner));
            return new UpsertChatResponse(ChatActionStatus.Success, await _chatViewModel.CreateChatDetailViewModeAsync(chat));
        }
        return new UpsertChatResponse(ChatActionStatus.Exception, null);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<UpsertChatResponse> UpdateAsync(UpsertChatViewModel update, Guid userId)
    {
        ChatsUsers? chatUser = await _chatsUsersQuery.GetAsync(cu => cu.ChatId == update.Id && cu.UserId == userId);
        if (chatUser != null && (chatUser.Type == (short)ChatUserType.Admin || chatUser.Type == (short)ChatUserType.Owner))
        {
            var chat = await _groupGet.GetChatAsync(update.Id ?? Guid.Empty);
            if (chat != null)
            {
                chat.UpdateDate = DateTime.UtcNow;
                chat.Description = update.Description;
                chat.Name = update.Name;

                return await _groupChatCud.UpdateAsync(chat) ?
                  new UpsertChatResponse(ChatActionStatus.Success, await _groupGet.GetChatDetailAsync(update.Id ?? Guid.Empty)) :
                      new UpsertChatResponse(ChatActionStatus.Exception, null);
            }
            return new UpsertChatResponse(ChatActionStatus.ChatNotFound, null);
        }
        return new UpsertChatResponse(ChatActionStatus.AccessDenied, null);
    }

    public async Task<UpsertChatResponse> UpsertChatAsync(UpsertChatViewModel upsert, IHeaderDictionary headers)
    {
        try
        {
            User? user = await _userGet.GetUserBySessionAsync(headers["Auth-Token"].ToString() ?? "");
            if (user == null)
                return new UpsertChatResponse(ChatActionStatus.UserNotAuthorized, null);

            return upsert.Id == null ?
                    await CreateAsync(upsert, user.Id) :
                        await UpdateAsync(upsert, user.Id);
        }
        catch
        {
            return new UpsertChatResponse(ChatActionStatus.Exception, null);
        }
    }
}
