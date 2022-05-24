using Identity.Client.Models;
using Identity.Client.Rules;
using Identity.Service.Tools.Code;
using Microsoft.AspNetCore.Http;


namespace ChTi.Service.Implemention;

public class ChatAction : IChatAction
{
    readonly IUserGet _userGet;

    readonly IBaseCud<Chat> _chatCud;

    readonly IBaseCud<ChatsUsers> _chatsUsersCud;

    readonly IBaseQuery<ChatsUsers> _chatsUsersQuery;

    readonly IChatViewModel _chatViewModel;

    readonly IChatGet _chatGet;

    public ChatAction(IUserGet userGet, IBaseCud<Chat> chatCud, IChatViewModel chatViewModel,
        IBaseCud<ChatsUsers> chatsUsersCud, IChatGet chatGet, IBaseQuery<ChatsUsers> chatsUsersQuery)
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
        Chat chat = new()
        {
            CreateDate = DateTime.Now,
            Description = create.Description,
            Name = create.Name,
            Status = (short)ChatStatus.Active,
            Token = 60.CreateToken(),
            Type = (short)create.Type,
            UpdateDate = DateTime.Now
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
            UpdateDefinition<Chat>? updateDefinition = Builders<Chat>.Update
                  .Set("name", update.Name).Set("description", update.Description)
                    .Set("updateDate", DateTime.Now);

            return await _chatCud.UpdateAsync(c => c.Id == update.Id, updateDefinition) ?
                    new UpsertChatResponse(ChatActionStatus.Success, await _chatGet.GetChatDetailAsync(update.Id ?? Guid.Empty)) :
                        new UpsertChatResponse(ChatActionStatus.Exception, null);
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
