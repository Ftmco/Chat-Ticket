﻿using ChTi.DataBase.Entity;
using ChTi.DataBase.ViewModel;
using Identity.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Abstraction;

public interface ITicketViewModel : IAsyncDisposable
{
    Task<TicketViewModel> CreateTicketViewModelAsync(Ticket ticket);

    Task<IEnumerable<TicketViewModel>> CreateTicketViewModelAsync(IEnumerable<Ticket> tickets);

    Task<TicketDetailViewModel> CreateTicketDetailViewModelAsync(Ticket ticket);

    Task<UserViewModel?> CreateUserViewModelAsync(User? user);
}
