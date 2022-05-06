using ChTi.DataBase.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Abstraction;

public interface ITicketGet : IAsyncDisposable
{
    Task<IEnumerable<Ticket>> GetTicketsAsync(HttpContext httpContext);
}
