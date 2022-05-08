using ChTi.DataBase.Entity;
using ChTi.DataBase.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Abstraction;

public interface ITicketGet : IAsyncDisposable
{
    Task<IEnumerable<TicketViewModel>> GetTicketsAsync(HttpContext httpContext);
}
