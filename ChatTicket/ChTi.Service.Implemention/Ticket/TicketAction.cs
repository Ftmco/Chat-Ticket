using Identity.Client.Rules;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace ChTi.Service.Implemention;

public class TicketAction : ITicketAction
{
    readonly IBaseCud<Ticket,TicketContext> _ticketCud;

    readonly IBaseCud<Attachment, TicketContext> _attachmentCud;

    readonly IBaseQuery<Ticket, TicketContext> _ticketQuery;

    readonly IUserGet _userGet;

    readonly ITicketViewModel _ticketViewModel;

    public TicketAction(IBaseCud<Ticket, TicketContext> ticketCud, IUserGet userGet, IBaseQuery<Ticket, TicketContext> ticketQuery,
        IBaseCud<Attachment, TicketContext> attachmentCud, ITicketViewModel ticketViewModel)
    {
        _ticketCud = ticketCud;
        _userGet = userGet;
        _ticketQuery = ticketQuery;
        _attachmentCud = attachmentCud;
        _ticketViewModel = ticketViewModel;
    }

    public async Task<AddAttachmentStatus> AddAttachmentAsync(HttpContext httpContext, AddAttachments addAttachments)
    {
        var session = httpContext.Request.Headers["Auth-Token"];
        if (!string.IsNullOrEmpty(session))
        {
            var user = await _userGet.GetUserBySessionAsync(session);
            if (user != null)
            {
                var ticket = await _ticketQuery.GetAsync(t => t.Id == addAttachments.TicketId);
                if (ticket != null)
                {
                    if (ticket.FromUserId != user.Id)
                        return AddAttachmentStatus.UserNotFound;

                    foreach (var item in addAttachments.Files)
                        await _attachmentCud.InsertAsync(new Attachment
                        {
                            FileId = item.FileId ?? Guid.Empty,
                            FileToken = item.FileToken,
                            ObjectId = ticket.Id
                        });
                    return AddAttachmentStatus.Success;
                }
                return AddAttachmentStatus.TicketNotFound;
            }
            return AddAttachmentStatus.UserNotFound;
        }
        return AddAttachmentStatus.UserNotFound;
    }

    public async Task<TicketActionStatus> ChangeTicketStatusAsync(HttpContext httpContext, Guid id, TicketStatus status)
    {
        var session = httpContext.Request.Headers["Auth-Token"];
        if (!string.IsNullOrEmpty(session))
        {
            var user = await _userGet.GetUserBySessionAsync(session);
            if (user != null)
            {
                var ticket = await _ticketQuery.GetAsync(t => t.Id == id && t.FromUserId == user.Id);
                if (ticket != null)
                {
                    ticket.Status = (short)status;
                    return await _ticketCud.UpdateAsync(ticket) ?
                            TicketActionStatus.Success : TicketActionStatus.Exception;
                }
                return TicketActionStatus.TicketNotFound;
            }
            return TicketActionStatus.UserNotfound;
        }
        return TicketActionStatus.UserNotfound;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<UpsertTicketResponse> UpsertTicketAsync(HttpContext httpContext, UpsertTicket upsertTicket)
    {
        var session = httpContext.Request.Headers["Auth-Token"];
        if (!string.IsNullOrEmpty(session))
        {
            var user = await _userGet.GetUserBySessionAsync(session);
            if (user != null)
            {
                Ticket ticket = new()
                {
                    Description = upsertTicket.Description,
                    FromUserId = user.Id,
                    Subject = upsertTicket.Subject,
                    ToUserId = upsertTicket.ToUser,
                    CreateDate = DateTime.UtcNow,
                    Status = (short)TicketStatus.Open
                };
                return await _ticketCud.InsertAsync(ticket) ?
                        new UpsertTicketResponse(TicketActionStatus.Success, await _ticketViewModel.CreateTicketViewModelAsync(ticket)) :
                            new UpsertTicketResponse(TicketActionStatus.Exception, null);
            }
            return new UpsertTicketResponse(TicketActionStatus.UserNotfound, null);
        }
        return new UpsertTicketResponse(TicketActionStatus.UserNotfound, null);
    }
}
