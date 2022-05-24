using ChTi.DataBase.Entity;
using ChTi.Service.Abstraction.Base;
using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    readonly IChatGet _chatGet;

    readonly IChatAction _chatAction;

    public ChatController(IChatGet chatGet,IChatAction chatAction)
    {
        _chatGet = chatGet;
        _chatAction = chatAction;
    }

    [HttpGet("ChatDetail")]
    public async Task<IActionResult> GetChatDetailAsync(string chatToken)
    {
        ChatDetailViewModel? chat = await _chatGet.GetChatDetailAsync(chatToken);
        return Ok(chat != null? Success($"جزئیات گفنگو {chat.Name}", "", chat) : Faild(404,"گفنگو یافت نشد",""));
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertAsync(UpsertChatViewModel upsert)
    {
        UpsertChatResponse chat = await _chatAction.UpsertChatAsync(upsert, Request.Headers);

        return chat.Status switch
        {
            ChatActionStatus.Success => Ok(Success("گفتگو با موفقیت ثبت شد","",chat.Chat)),
            ChatActionStatus.UserNotAuthorized => Ok(Faild(403,"برای ایجاد گفتگو وارد حساب خود شوید","")),
            ChatActionStatus.Exception => Ok(ApiException()),
            ChatActionStatus.ChatNotFound => Ok(Faild(404,"گفتگو یافت نشد","")),
            _ => Ok(ApiException()),
        };
    }

    [HttpGet("Chats")]
    public async Task<IActionResult> GetChatsAsync()
    {
        IEnumerable<ChatDetailViewModel> chats = await _chatGet.GetUserChatsAsync(Request.Headers);
        return Ok(Success("گفتگو های کاربر","",chats));
    }
}
