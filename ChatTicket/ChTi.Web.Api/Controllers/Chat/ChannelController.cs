using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers.Chat;

[Route("api/Chat/[controller]")]
[ApiController]
public class ChannelController : ChatBaseController
{
    readonly IChannelAction _channelAction;

    readonly IChannelGet _channelGet;

    public ChannelController(IChannelAction channelAction, IChannelGet channelGet)
    {
        _channelAction = channelAction;
        _channelGet = channelGet;
    }

    public override async Task<IActionResult> GetChatDetailAsync(string chatToken)
    {
        var channelDetail = await _channelGet.GetChatDetailAsync(chatToken);
        return Ok(Success("", "", channelDetail));
    }

    public async override Task<IActionResult> GetChatsAsync()
        => Ok(Success("کانال ها", "", await _channelGet.GetUserChannelsAsync(Request.Headers)));
}
