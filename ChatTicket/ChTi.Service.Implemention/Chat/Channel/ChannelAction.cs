using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class ChannelAction : IChannelAction
{
    public Task<UpsertChatResponse> CreateAsync(UpsertChatViewModel create, Guid userId)
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UpsertChatResponse> UpdateAsync(UpsertChatViewModel update, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<UpsertChatResponse> UpsertChatAsync(UpsertChatViewModel upsert, IHeaderDictionary headers)
    {
        throw new NotImplementedException();
    }
}
