using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Abstraction;

public interface IChannelGet : IChatBaseGet<ChannelChat>
{
    Task<IEnumerable<ChatDetailViewModel>> GetUserChannelsAsync(IHeaderDictionary headers);

    Task<ChatDetailViewModel?> GetChatDetailAsync(string chatToken);

    Task<ChatDetailViewModel?> GetChatDetailAsync(Guid chatId);
}
