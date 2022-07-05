﻿using ChTi.DataBase.Entity;
using ChTi.DataBase.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Abstraction;

public interface IChatGet : IAsyncDisposable
{
    Task<ChatDetailViewModel?> GetChatDetailAsync(string chatToken);

    Task<ChatDetailViewModel?> GetChatDetailAsync(Guid chatId);

    Task<Chat?> GetChatAsync(string chatToken);

    Task<Chat?> GetChatAsync(Guid chatId);

    Task<IEnumerable<ChatDetailViewModel>> GetUserChatsAsync(IHeaderDictionary headers);

    Task<IEnumerable<PvChatDetailViewModel>> GetUserPvChatsAsync(IHeaderDictionary headers);

    Task<IEnumerable<ChatDetailViewModel>> GetUserGroupsAsync(IHeaderDictionary headers);

    Task<IEnumerable<ChatDetailViewModel>> GetUserChannelsAsync(IHeaderDictionary headers);

    Task<bool> UserInChatAsync(Guid chatId, Guid userId);
}
