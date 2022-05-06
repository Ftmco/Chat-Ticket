using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.ViewModel;

public record TicketViewModel(Guid Id, Guid FromId, Guid ToId, string Subject, string Description, IEnumerable<UploadFileViewModel> Files);

public record UpsertTicket(Guid? Id, Guid ToUser, string Subject, string Description);

public record AddAttachments(Guid TicketId,IEnumerable<UploadFileViewModel> Files);

public record UpsertTicketResponse(UpsertTicketStatus Status, TicketViewModel? Ticket);

public enum UpsertTicketStatus
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