using ChTi.DataBase.Entity;
using ChTi.DataBase.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Abstraction;

public interface ITicketAction : IAsyncDisposable
{
    Task<UpsertTicketResponse> UpsertTicketAsync(HttpContext httpContext, UpsertTicket upsertTicket);

    Task<AddAttachmentStatus> AddAttachmentAsync(HttpContext httpContext, AddAttachments addAttachments);

    Task<TicketActionStatus> ChangeTicketStatusAsync(HttpContext httpContext, Guid id, TicketStatus status);
}
