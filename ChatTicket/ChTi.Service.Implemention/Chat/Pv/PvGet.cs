using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class PvGet : IPvGet
{
    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
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

    public Task<IEnumerable<PvChatDetailViewModel>> GetUserPvChatsAsync(IHeaderDictionary headers)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserInChatAsync(Guid chatId, Guid userId)
    {
        throw new NotImplementedException();
    }
}
