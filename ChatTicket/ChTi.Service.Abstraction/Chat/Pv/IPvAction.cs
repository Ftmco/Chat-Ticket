
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Abstraction;

public interface IPvAction : IChatBaseAction
{
    Task<PvChatResponse> StartPvChatAsync(IHeaderDictionary headers, Guid userId);
}
