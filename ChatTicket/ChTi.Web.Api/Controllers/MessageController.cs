using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    [HttpGet("GetMessages")]
    public async Task<IActionResult> GetMessagesAsync(Guid chatId, long lastMessageId)
    {
        return Ok();
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessageAsync()
    {
        return Ok();
    }

    [HttpPut("UpdateMessage")]
    public async Task<IActionResult> UpdateMessageAsync()
    {
        return Ok();
    }

    [HttpDelete("DeleteMessage")]
    public async Task<IActionResult> DeleteMessageAsync()
    {
        return Ok();
    }
}
