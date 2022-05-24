using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class ChatUserAction : IChatUserAction
{
    readonly IBaseCud<ChatsUsers> _chatsUsersCud;

    public ChatUserAction(IBaseCud<ChatsUsers> chatsUsersCud)
    {
        _chatsUsersCud = chatsUsersCud;
    }

    public async Task<ChatsUsers?> AddUserToChatAsync(AddUserToChatViewModel addUserToChat)
    {
        ChatsUsers chatsUsers = CreateChatUser(addUserToChat);
        return await _chatsUsersCud.InsertAsync(chatsUsers) ?
                chatsUsers : null;
    }

    public async Task<IEnumerable<ChatsUsers>> AddUserToChatAsync(IEnumerable<AddUserToChatViewModel> addUsersToChats)
    {
        List<ChatsUsers> chatsUsers = new();
        foreach (var user in addUsersToChats)
            chatsUsers.Add(CreateChatUser(user));

        await _chatsUsersCud.InsertAsync(chatsUsers);
        return chatsUsers;
    }

    static ChatsUsers CreateChatUser(AddUserToChatViewModel addUserToChat)
        => new()
        {
            ChatId = addUserToChat.ChatId ?? Guid.Empty,
            UserId = addUserToChat.UserId,
            Type = (short)addUserToChat.UserType,
            JoinDate = DateTime.Now,
        };

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
