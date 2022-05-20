﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.ViewModel;

public record TicketViewModel(Guid Id, Guid FromId, Guid ToId, string Subject, string Description, string CreateDate, TicketStatusViewModel Status);

public record UpsertTicket(Guid? Id, Guid ToUser, string Subject, string Description);

public record AddAttachments(Guid TicketId, IEnumerable<FileViewModel> Files);

public record UpsertTicketResponse(TicketActionStatus Status, TicketViewModel? Ticket);

public record TicketStatusViewModel(short Code, string Name);

public record TicketDetialViewModel(TicketViewModel? Ticket, UserViewModel? FromUser, UserViewModel? ToUser);

public record GetTicketDetial(TicketActionStatus Status, TicketDetialViewModel? Ticket);

public enum TicketActionStatus
{
    Success,
    UserNotfound,
    Exception,
    TicketNotFound
}

public enum AddAttachmentStatus
{
    Success,
    UserNotFound,
    Exception,
    TicketNotFound
}