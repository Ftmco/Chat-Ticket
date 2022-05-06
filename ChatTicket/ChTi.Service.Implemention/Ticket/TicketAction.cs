using Identity.Client.Rules;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class TicketAction : ITicketAction
{
    readonly IBaseCud<Ticket> _ticketCud;

    readonly IBaseCud<Attachment> _attachmentCud;

    readonly IBaseQuery<Ticket> _ticketQuery;

    readonly IUserGet _userGet;

    public TicketAction(IBaseCud<Ticket> ticketCud, IUserGet userGet, IBaseQuery<Ticket> ticketQuery,
        IBaseCud<Attachment> attachmentCud)
    {
        _ticketCud = ticketCud;
        _userGet = userGet;
        _ticketQuery = ticketQuery;
        _attachmentCud = attachmentCud;
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
                            TicketId = ticket.Id
                        });
                    return AddAttachmentStatus.Success;
                }
                return AddAttachmentStatus.TicketNotFound;
            }
            return AddAttachmentStatus.UserNotFound;
        }
        return AddAttachmentStatus.UserNotFound;
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
                    CreateDate = DateTime.Now
                };
                return await _ticketCud.InsertAsync(ticket) ?
                        new UpsertTicketResponse(UpsertTicketStatus.Success, null) :
                            new UpsertTicketResponse(UpsertTicketStatus.Exception, null);
            }
            return new UpsertTicketResponse(UpsertTicketStatus.UserNotfound, null);
        }
        return new UpsertTicketResponse(UpsertTicketStatus.UserNotfound, null);
    }
}
