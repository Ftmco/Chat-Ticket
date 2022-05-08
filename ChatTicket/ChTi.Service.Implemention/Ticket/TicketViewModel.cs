using ChTi.Service.Tools.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class TicketViewModel : ITicketViewModel
{
    public Task<DataBase.ViewModel.TicketViewModel> CreateTicketViewModelAsync(Ticket ticket)
    {
        DataBase.ViewModel.TicketViewModel viewModel = new(
            Id: ticket.Id,
            FromId: ticket.FromUserId,
            ToId: ticket.ToUserId, Subject: ticket.Subject,
            Description: ticket.Description,
            CreateDate: ticket.CreateDate.ToShamsi(),
            Status: new(ticket.Status, (TicketStatus)ticket.Status switch
            {
                TicketStatus.Open => "باز",
                TicketStatus.Close => "بسته شده",
                _ => "باز"
            }),
            Files: default);
        return Task.FromResult(viewModel);
    }

    public async Task<IEnumerable<DataBase.ViewModel.TicketViewModel>> CreateTicketViewModelAsync(IEnumerable<Ticket> tickets)
    {
        List<DataBase.ViewModel.TicketViewModel> result = new();
        foreach (var item in tickets)
            result.Add(await CreateTicketViewModelAsync(item));
        return result;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
