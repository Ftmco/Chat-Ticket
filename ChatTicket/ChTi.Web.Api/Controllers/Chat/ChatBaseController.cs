using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers.Chat;

public abstract class ChatBaseController : ControllerBase
{
    [HttpGet("GetChatDetail")]
    public async virtual Task<IActionResult> GetChatDetailAsync(string chatToken) => Ok();

    [HttpGet("Chats")]
    public async virtual Task<IActionResult> GetChatsAsync() => Ok();
}
