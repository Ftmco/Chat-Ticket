using ChTi.Service.Tools.Date;
using Identity.Client.Models;
using Identity.Client.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class TicketViewModel : ITicketViewModel
{
    readonly IUserGet _userGet;

    public TicketViewModel(IUserGet userGet)
    {
        _userGet = userGet;
    }

    public async Task<TicketDetailViewModel> CreateTicketDetailViewModelAsync(Ticket ticket)
    {
        User? fromUser = await _userGet.GetUserByIdAsync(ticket.FromUserId);
        User? toUser = await _userGet.GetUserByIdAsync(ticket.ToUserId);
        TicketDetailViewModel ticketDetail = new(
            Ticket: await CreateTicketViewModelAsync(ticket),
            FromUser: await CreateUserViewModelAsync(fromUser),
            ToUser: await CreateUserViewModelAsync(toUser));
        return ticketDetail;
    }

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
            }));
        return Task.FromResult(viewModel);
    }

    public async Task<IEnumerable<DataBase.ViewModel.TicketViewModel>> CreateTicketViewModelAsync(IEnumerable<Ticket> tickets)
    {
        List<DataBase.ViewModel.TicketViewModel> result = new();
        foreach (var item in tickets)
            result.Add(await CreateTicketViewModelAsync(item));
        return result;
    }

    public async Task<UserViewModel?> CreateUserViewModelAsync(User? user)
    {
        if (user == null)
            return null;

        IEnumerable<Avatar>? userAvatars = await _userGet.GetUserAvatarsAsync(user.Id, false);
        UserViewModel userViewModel = new(
            Id: user.Id,
            FullName: user.FullName,
            Email: user.Email,
            MobileNo: user.MobileNo,
            Avatar: userAvatars?.Select((avatr) =>
                            new FileViewModel(FileId: avatr.FileId, FileToken: avatr.FileToken)));
        return userViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
