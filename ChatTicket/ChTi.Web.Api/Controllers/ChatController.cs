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

    public ChatController(IChatGet chatGet, IChatAction chatAction)
    {
        _chatGet = chatGet;
        _chatAction = chatAction;
    }

    [HttpGet("ChatDetail")]
    public async Task<IActionResult> GetChatDetailAsync(string chatToken,ChatType chatType)
    {
        ChatDetailViewModel? chat = await _chatGet.GetChatDetailAsync(chatToken);
        return Ok(chat != null ? Success($"جزئیات گفنگو {chat.Name}", "", chat) : Faild(404, "گفنگو یافت نشد", ""));
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertAsync(UpsertChatViewModel upsert)
    {
        UpsertChatResponse chat = await _chatAction.UpsertChatAsync(upsert, Request.Headers);

        return chat.Status switch
        {
            ChatActionStatus.Success => Ok(Success("گفتگو با موفقیت ثبت شد", "", chat.Chat)),
            ChatActionStatus.UserNotAuthorized => Ok(Faild(403, "برای ایجاد گفتگو وارد حساب خود شوید", "")),
            ChatActionStatus.Exception => Ok(ApiException()),
            ChatActionStatus.ChatNotFound => Ok(Faild(404, "گفتگو یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }

    [HttpGet("Chats")]
    public async Task<IActionResult> GetChatsAsync(ChatType chatType)
    {
        return chatType switch
        {
            ChatType.Group => Ok(Success("گروه ها", "", await _chatGet.GetUserGroupsAsync(Request.Headers))),
            ChatType.Pv => Ok(Success("گفتگو های خصوصی", "", await _chatGet.GetUserPvChatsAsync(Request.Headers))),
            ChatType.Channel => Ok(Success("کانال ها", "", await _chatGet.GetUserChannelsAsync(Request.Headers))),
            _ => Ok(ApiException()),
        };
    }

    [HttpGet("StartPvChat")]
    public async Task<IActionResult> PvChatAsync(Guid userId)
    {
        PvChatResponse pvChat = await _chatAction.StartPvChatAsync(Request.Headers, userId);
        return pvChat.Status switch
        {
            ChatActionStatus.Success => Ok(Success("", "", pvChat.Chat)),
            ChatActionStatus.UserNotAuthorized => Ok(Faild(403, "برای ایجاد گفتگو وارد حساب خود شوید", "")),
            ChatActionStatus.Exception => Ok(ApiException()),
            ChatActionStatus.ChatNotFound => Ok(Faild(404, "گفتگو یافت نشد", "")),
            ChatActionStatus.AccessDenied => Ok(Faild(403, "برای ایجاد گفتگو وارد حساب خود شوید", "")),
            ChatActionStatus.UserNotFound => Ok(Faild(404, "کاربر مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }
}
