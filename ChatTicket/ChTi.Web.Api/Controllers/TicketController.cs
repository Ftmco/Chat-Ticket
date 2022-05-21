using ChTi.DataBase.Entity;
using ChTi.Service.Abstraction;
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
            TicketActionStatus.Success => Ok(Success("تیکت با موفقیت ثبت شد", "", upsert.Ticket)),
            TicketActionStatus.UserNotfound => Ok(Faild(403, "برای ایجاد تیکت وارد حساب خود شوید", "")),
            TicketActionStatus.Exception => Ok(ApiException()),
            TicketActionStatus.TicketNotFound => Ok(Faild(404, "تیکت مورد نظر یافت نشد", "")),
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
            AddAttachmentStatus.TicketNotFound => Ok(Faild(404, "تیکت مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        TicketActionStatus delete = await _ticketAction.ChangeTicketStatusAsync(HttpContext, id, TicketStatus.Deleted);
        return delete switch
        {
            TicketActionStatus.Success => Ok(Success("تیکت با موفقیت حذف شد", "", new { id })),
            TicketActionStatus.UserNotfound => Ok(Faild(403, "برای ایجاد تیکت وارد حساب خود شوید", "")),
            TicketActionStatus.Exception => Ok(ApiException()),
            TicketActionStatus.TicketNotFound => Ok(Faild(404, "تیکت مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }

    [HttpDelete("Close")]
    public async Task<IActionResult> CloseAsync(Guid id)
    {
        var close = await _ticketAction.ChangeTicketStatusAsync(HttpContext, id, TicketStatus.Close);
        return close switch
        {
            TicketActionStatus.Success => Ok(Success("تیکت با موفقیت بسته شد", "", new { id })),
            TicketActionStatus.UserNotfound => Ok(Faild(403, "برای ایجاد تیکت وارد حساب خود شوید", "")),
            TicketActionStatus.Exception => Ok(ApiException()),
            TicketActionStatus.TicketNotFound => Ok(Faild(404, "تیکت مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }

    [HttpGet("GetTicket")]
    public async Task<IActionResult> GetTicketAsync(Guid ticketId)
    {
        var ticket = await _ticketGet.GetTicketAsync(ticketId, Request.Headers);
        return ticket.Status switch
        {
            TicketActionStatus.Success => Ok(Success("جزئیات تیکت", "", ticket.Ticket)),
            TicketActionStatus.UserNotfound => Ok(Faild(403, "برای مشاهده تیکت وارد حساب خود شوید", "")),
            TicketActionStatus.Exception => Ok(ApiException()),
            TicketActionStatus.TicketNotFound => Ok(Faild(404, "تیکت مورد نظر یافت نشد", "")),
            _ => Ok(ApiException()),
        };
    }
}
