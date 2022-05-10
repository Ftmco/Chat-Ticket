using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    [HttpGet("Comments")]
    public async Task<IActionResult> GetCommentsAsync(Guid objectId)
    {
        return Ok();
    }

    [HttpPost("SendComment")]
    public async Task<IActionResult> SendCommentAsync(SendCommentViewModel sendComment)
    {
        return Ok();
    }
}
