using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class GroupGet : IGroupGet
{
    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GroupChat?> GetChatAsync(string chatToken)
    {
        throw new NotImplementedException();
    }

    public Task<GroupChat?> GetChatAsync(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<ChatDetailViewModel?> GetChatDetailAsync(string chatToken)
    {
        throw new NotImplementedException();
    }

    public Task<ChatDetailViewModel?> GetChatDetailAsync(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ChatDetailViewModel>> GetUserGroupsAsync(IHeaderDictionary headers)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserInChatAsync(Guid chatId, Guid userId)
    {
        throw new NotImplementedException();
    }
}
