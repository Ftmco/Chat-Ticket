using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers.Chat;

[Route("api/Chat/[controller]")]
[ApiController]
public class GroupController : ChatBaseController
{
    readonly IGroupGet _groupGet;

    readonly IGroupAction _groupAction;

    public GroupController(IGroupGet groupGet, IGroupAction groupAction)
    {
        _groupGet = groupGet;
        _groupAction = groupAction;
    }

    public override async Task<IActionResult> GetChatDetailAsync(string chatToken)
    {
        var groupDetail = await _groupGet.GetChatDetailAsync(chatToken);
        return Ok(Success("", "", groupDetail));
    }

    public async override Task<IActionResult> GetChatsAsync()
        => Ok(Success("گروه ها", "", await _groupGet.GetUserGroupsAsync(Request.Headers)));
}
