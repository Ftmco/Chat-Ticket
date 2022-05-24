using Identity.Client.Models;
using Identity.Client.Rules;
using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Implemention;

public class ChatGet : IChatGet
{
    readonly IChatViewModel _chatViewModel;

    readonly IBaseQuery<Chat> _chatQuery;

    readonly IBaseQuery<ChatsUsers> _chatsUsersQuery;

    readonly IUserGet _userGet;

    public ChatGet(IChatViewModel chatViewModel, IBaseQuery<Chat> chatQuery, IUserGet userGet, IBaseQuery<ChatsUsers> chatsUsersQuery)
    {
        _chatViewModel = chatViewModel;
        _chatQuery = chatQuery;
        _userGet = userGet;
        _chatsUsersQuery = chatsUsersQuery;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<Chat?> GetChatAsync(string chatToken)
        => await _chatQuery.GetAsync(c => c.Token == chatToken);

    public async Task<Chat?> GetChatAsync(Guid chatId)
        => await _chatQuery.GetAsync(c => c.Id == chatId);

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

    public async Task<IEnumerable<ChatDetailViewModel>> GetUserChatsAsync(IHeaderDictionary headers)
    {
        User? user = await _userGet.GetUserBySessionAsync(headers["Auth-Token"].ToString() ?? "");
        List<ChatDetailViewModel> chats = new();
        if (user != null)
        {
            IEnumerable<ChatsUsers>? userChats = await _chatsUsersQuery.GetAllAsync(uc => uc.UserId == user.Id);
            foreach (ChatsUsers? userChat in userChats)
            {
                Chat? chat = await _chatQuery.GetAsync(c => c.Id == userChat.ChatId);
                if (chat != null)
                {
                    ChatDetailViewModel? chatDetail = await _chatViewModel.CreateChatDetailViewModeAsync(chat);
                    if (chatDetail != null)
                        chats.Add(chatDetail);
                }
            }
        }
        return chats;
    }

    public async Task<bool> UserInChatAsync(Guid chatId, Guid userId)
    {
        ChatsUsers? userChat = await _chatsUsersQuery.GetAsync(cu => cu.ChatId == chatId && cu.UserId == userId);
        return userChat != null;
    }
}
