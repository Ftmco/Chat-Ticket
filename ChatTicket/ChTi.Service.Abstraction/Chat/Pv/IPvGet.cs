using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Abstraction;

public interface IPvGet : IChatBaseGet<PvChat>
{
    Task<IEnumerable<PvChatDetailViewModel>> GetUserPvChatsAsync(IHeaderDictionary headers);

    Task<PvChatDetailViewModel?> GetChatDetailAsync(string chatToken);

    Task<PvChatDetailViewModel?> GetChatDetailAsync(Guid chatId);
}
