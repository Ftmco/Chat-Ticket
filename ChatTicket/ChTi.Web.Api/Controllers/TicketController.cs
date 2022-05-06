using ChTi.Service.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChTi.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    readonly ITicketGet _ticketGet;

    readonly ITicketAction _ticketAction;

    public TicketController(ITicketGet ticketGet, ITicketAction ticketAction)
    {
        _ticketGet = ticketGet;
        _ticketAction = ticketAction;
    }

    [HttpGet("GetTickets")]
    public async Task<IActionResult> GetTicketsAsync()
    {
        var tickets = await _ticketGet.GetTicketsAsync(HttpContext);
        return Ok(Success("تیکت ها", "", tickets));
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> UpsertAsync(UpsertTicket upsertTicket)
    {
        UpsertTicketResponse upsert = await _ticketAction.UpsertTicketAsync(HttpContext, upsertTicket);
        return upsert.Status switch
        {
            UpsertTicketStatus.Success => Ok(Success("تیکت با موفقیت ثبت شد", "", upsert.Ticket)),
            UpsertTicketStatus.UserNotfound => Ok(Faild(403, "برای ایجاد تیکت وارد حساب خود شوید", "")),
            UpsertTicketStatus.Exception => Ok(ApiException()),
            UpsertTicketStatus.TicketNotFound => Ok(Faild(404, "نیکت مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }

    [HttpPost("AddAttachment")]
    public async Task<IActionResult> AddAttachmentAsync(AddAttachments attachments)
    {
        return await _ticketAction.AddAttachmentAsync(HttpContext, attachments) switch
        {
            AddAttachmentStatus.Success => Ok(Success("پیوست با موفیت ثبت شد", "", new { })),
            AddAttachmentStatus.UserNotFound => Ok(Faild(403, "برای ایجاد تیکت وارد حساب خود شوید", "")),
            AddAttachmentStatus.Exception => Ok(ApiException()),
            AddAttachmentStatus.TicketNotFound => Ok(Faild(404, "نیکت مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }
}
