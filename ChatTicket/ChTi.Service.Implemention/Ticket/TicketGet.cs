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

    readonly ITicketViewModel _ticketViewModel;

    public TicketGet(IBaseQuery<Ticket> ticketQuery, IUserGet userGet, ITicketViewModel ticketViewModel)
    {
        _ticketQuery = ticketQuery;
        _userGet = userGet;
        _ticketViewModel = ticketViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<GetTicketDetail> GetTicketAsync(Guid ticketId, IHeaderDictionary headers)
    {
        var user = await _userGet.GetUserBySessionAsync(headers["Auth-Token"].ToString() ?? "");
        if (user != null)
        {
            Ticket? ticket = await _ticketQuery.GetAsync(ticketId);
            if (ticket != null && (ticket.FromUserId == user.Id || ticket.ToUserId == user.Id))
            {
                TicketDetailViewModel? ticketDetail = await _ticketViewModel.CreateTicketDetailViewModelAsync(ticket);
                return new GetTicketDetail(TicketActionStatus.Success, ticketDetail);
            }
            return new GetTicketDetail(TicketActionStatus.TicketNotFound, null);
        }
        return new GetTicketDetail(TicketActionStatus.UserNotfound, null);
    }

    public async Task<IEnumerable<DataBase.ViewModel.TicketViewModel>> GetTicketsAsync(HttpContext httpContext)
    {
        var session = httpContext.Request.Headers["Auth-Token"];
        if (!string.IsNullOrEmpty(session))
        {
            var user = await _userGet.GetUserBySessionAsync(session);
            if (user != null)
            {
                var tickets = await _ticketQuery.GetAllAsync(t => (t.FromUserId == user.Id || t.ToUserId == user.Id) && t.Status != (short)TicketStatus.Deleted);
                return await _ticketViewModel.CreateTicketViewModelAsync(tickets);
            }
            return new List<DataBase.ViewModel.TicketViewModel>();
        }
        return new List<DataBase.ViewModel.TicketViewModel>();

    }
}
