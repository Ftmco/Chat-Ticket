using Identity.Client.Models;
using Identity.Client.Rules;
using Identity.Service.Tools.Code;
using Microsoft.AspNetCore.Http;


namespace ChTi.Service.Implemention;

public class ChatAction : IChatAction
{
    readonly IUserGet _userGet;

    readonly IBaseCud<ChatBase, ChatContext> _chatCud;

    readonly IBaseCud<ChatsUsers, ChatContext> _chatsUsersCud;

    readonly IBaseQuery<ChatsUsers, ChatContext> _chatsUsersQuery;

    readonly IChatViewModel _chatViewModel;

    readonly IChatGet _chatGet;

    public ChatAction(IUserGet userGet, IBaseCud<ChatBase, ChatContext> chatCud, IChatViewModel chatViewModel,
        IBaseCud<ChatsUsers, ChatContext> chatsUsersCud, IChatGet chatGet, IBaseQuery<ChatsUsers, ChatContext> chatsUsersQuery)
    {
        _userGet = userGet;
        _chatCud = chatCud;
        _chatViewModel = chatViewModel;
        _chatsUsersCud = chatsUsersCud;
        _chatGet = chatGet;
        _chatsUsersQuery = chatsUsersQuery;
    }

    public async Task<ChatsUsers?> AddUserToChatAsync(Guid chatId, Guid userId, ChatUserType userType)
    {
        ChatsUsers chatUser = new()
        {
            ChatId = chatId,
            UserId = userId,
            Type = (short)userType
        };
        return await _chatsUsersCud.InsertAsync(chatUser) ? chatUser : null;
    }

    public async Task<UpsertChatResponse> CreateAsync(UpsertChatViewModel create, Guid userId)
    {
        ChatBase chat = new()
        {
            CreateDate = DateTime.UtcNow,
            Description = create.Description,
            Name = create.Name,
            Status = (short)ChatStatus.Active,
            Token = 60.CreateToken(),
            Type = (short)create.Type,
            UpdateDate = DateTime.UtcNow
        };
        if (await _chatCud.InsertAsync(chat))
        {
            await AddUserToChatAsync(chat.Id, userId, ChatUserType.Owner);
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
            var chat = await _chatGet.GetChatAsync(update.Id ?? Guid.Empty);
            if (chat != null)
            {
                chat.UpdateDate = DateTime.UtcNow;
                chat.Description = update.Description;
                chat.Name = update.Name;

                return await _chatCud.UpdateAsync(chat) ?
                  new UpsertChatResponse(ChatActionStatus.Success, await _chatGet.GetChatDetailAsync(update.Id ?? Guid.Empty)) :
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
