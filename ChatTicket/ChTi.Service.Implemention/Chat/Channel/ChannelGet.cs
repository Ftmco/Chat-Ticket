using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class ChannelGet : IChannelGet
{
    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ChannelChat?> GetChatAsync(string chatToken)
    {
        throw new NotImplementedException();
    }

    public Task<ChannelChat?> GetChatAsync(Guid chatId)
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

    public Task<IEnumerable<ChatDetailViewModel>> GetUserChannelsAsync(IHeaderDictionary headers)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserInChatAsync(Guid chatId, Guid userId)
    {
        throw new NotImplementedException();
    }
}
