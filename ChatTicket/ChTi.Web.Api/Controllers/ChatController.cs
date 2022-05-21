using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    [HttpGet("ChatDetail")]
    public async Task<IActionResult> GetChatDetailAsync(Guid chatId)
    {
        return Ok();
    }
}
