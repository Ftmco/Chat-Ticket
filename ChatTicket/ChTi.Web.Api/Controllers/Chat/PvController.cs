using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers.Chat;

[Route("api/Chat/[controller]")]
[ApiController]
public class PvController : ChatBaseController
{
    readonly IPvGet _pvGet;

    readonly IPvAction _pvAction;

    public PvController(IPvGet pvGet, IPvAction pvAction)
    {
        _pvGet = pvGet;
        _pvAction = pvAction;
    }

    public override async Task<IActionResult> GetChatDetailAsync(string chatToken)
    {
        PvChatResponse? pvDetail = await _pvGet.GetChatDetailAsync(chatToken);
        return pvDetail.Status switch
        {
            ChatActionStatus.Success => Ok(Success("", "", pvDetail.Chat)),
            ChatActionStatus.UserNotAuthorized => Ok(Faild(403, "برای مشاهده جزئیات وارد حساب خود شوید", "")),
            ChatActionStatus.Exception => Ok(ApiException()),
            ChatActionStatus.ChatNotFound => Ok(Faild(404, "گفتگو یافت نشد", "")),
            ChatActionStatus.AccessDenied => Ok(Faild(403, "برای مشاهده جزئیات وارد حساب خود شوید", "")),
            ChatActionStatus.UserNotFound => Ok(Faild(403, "برای مشاهده جزئیات وارد حساب خود شوید", "")),
            _ => Ok(ApiException()),
        };
    }

    public async override Task<IActionResult> GetChatsAsync()
        => Ok(Success("گفتگو های خصوصی", "", await _pvGet.GetUserPvChatsAsync(Request.Headers)));

    [HttpGet("Start")]
    public async Task<IActionResult> PvChatAsync(Guid userId)
    {
        PvChatResponse pvChat = await _pvAction.StartPvChatAsync(Request.Headers, userId);
        return pvChat.Status switch
        {
            ChatActionStatus.Success => Ok(Success("", "", pvChat.Chat)),
            ChatActionStatus.UserNotAuthorized => Ok(Faild(403, "برای ایجاد گفتگو وارد حساب خود شوید", "")),
            ChatActionStatus.Exception => Ok(ApiException()),
            ChatActionStatus.ChatNotFound => Ok(Faild(404, "گفتگو یافت نشد", "")),
            ChatActionStatus.AccessDenied => Ok(Faild(403, "برای ایجاد گفتگو وارد حساب خود شوید", "")),
            ChatActionStatus.UserNotFound => Ok(Faild(404, "کاربر مورد نظر یافت نشد", "")),
            ChatActionStatus.Illegal => Ok(Faild(409,"ایجاد گفتگو مجاز نیست","")),
            _ => Ok(ApiException()),
        };
    }
}
