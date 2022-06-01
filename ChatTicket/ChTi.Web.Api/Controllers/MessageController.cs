using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    readonly IMessageGet _messageGet;

    readonly IMessageAction _messageAction;

    public MessageController(IMessageGet messageGet, IMessageAction messageAction)
    {
        _messageGet = messageGet;
        _messageAction = messageAction;
    }

    [HttpGet("GetMessages")]
    public async Task<IActionResult> GetMessagesAsync(string chatToken, long lastMessageId)
    {
        IEnumerable<MessageViewModel> messages = await _messageGet.GetMessagesAsync(chatToken, lastMessageId, Request.Headers);
        return Ok(Success("", "", messages));
    }

    [HttpGet("LastMessages")]
    public async Task<IActionResult> GetLastMessagesAsync(string chatToken,int count, long? lastMessageId)
    {
        IEnumerable<MessageViewModel> messages = await _messageGet.GetLastMessagesAsync(chatToken, count,Request.Headers, lastMessageId);
        return Ok(Success("", "", messages));
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessageAsync(SendMessageViewModel sendMessage)
    {
        SendMessageResponse message = await _messageAction.SendMessageAsync(sendMessage, Request.Headers);
        return message.Status switch
        {
            MessageActionStatus.Success => Ok(Success("پیام با موفقیت ارسال شد", "", message.Message)),
            MessageActionStatus.AccessDenied => Ok(Faild(403, "شما برای ارسال پیام به این گفتگو دسترسی ندارید", "")),
            MessageActionStatus.Exception => Ok(ApiException()),
            MessageActionStatus.MessageNotFound => Ok(Faild(404, "پیام مورد نظر یافت نشد", "")),
            MessageActionStatus.ChatNotFound => Ok(Faild(404, "گفتگو یافت نشد", "")),
            _ => Ok(ApiException()),
        };
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
