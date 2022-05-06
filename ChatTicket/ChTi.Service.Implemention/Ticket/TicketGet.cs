using ChTi.DataBase.Entity;
using ChTi.Service.Abstraction;
using Identity.Client.Rules;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class TicketGet : ITicketGet
{
    readonly IBaseQuery<Ticket> _ticketQuery;

    readonly IUserGet _userGet;

    public TicketGet(IBaseQuery<Ticket> ticketQuery, IUserGet userGet)
    {
        _ticketQuery = ticketQuery;
        _userGet = userGet;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<IEnumerable<Ticket>> GetTicketsAsync(HttpContext httpContext)
    {
        var session = httpContext.Request.Headers["Auth-Token"];
        if (!string.IsNullOrEmpty(session))
        {
            var user = await _userGet.GetUserBySessionAsync(session);
            if (user != null)
            {
                var tickets = await _ticketQuery.GetAllAsync(t => t.FromUserId == user.Id || t.ToUserId == user.Id);
                return tickets;
            }
            return new List<Ticket>();
        }
        return new List<Ticket>();

    }
}
