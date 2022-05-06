using ChTi.DataBase.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Abstraction;

public interface ITicketAction : IAsyncDisposable
{
    Task<UpsertTicketResponse> UpsertTicketAsync(HttpContext httpContext, UpsertTicket upsertTicket);

    Task<AddAttachmentStatus> AddAttachmentAsync(HttpContext httpContext, AddAttachments addAttachments);
}
