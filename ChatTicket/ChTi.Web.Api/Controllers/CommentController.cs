using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    [HttpPost("UpsertComment")]
    public async Task<IActionResult> UpsertCommentAsync()
    {
        return
            Ok();
    }

    [HttpGet("GetComments")]
    public async Task<IActionResult> GetCommentsAsync(Guid objectId)
    {
        return Ok();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteCommentAsync(Guid commentId)
    {
        return Ok();
    }

    [HttpGet("GetCommentReply")]
    public async Task<IActionResult> GetCommentReplyAsync(Guid commentId)
    {
        return Ok();
    }
}
