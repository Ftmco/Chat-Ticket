using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class PvAction : IPvAction
{
    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PvChatResponse> StartPvChatAsync(IHeaderDictionary headers, Guid userId)
    {
        throw new NotImplementedException();
    }
}
