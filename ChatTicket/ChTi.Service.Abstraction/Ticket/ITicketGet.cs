using ChTi.DataBase.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Abstraction;

public interface ITicketGet : IAsyncDisposable
{
    Task<IEnumerable<TicketViewModel>> GetTicketsAsync(HttpContext httpContext);

    Task<GetTicketDetail> GetTicketAsync(Guid ticketId, IHeaderDictionary headers);
}
