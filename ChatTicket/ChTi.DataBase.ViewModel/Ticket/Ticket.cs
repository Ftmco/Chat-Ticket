namespace ChTi.DataBase.ViewModel;

public record TicketViewModel(Guid Id, Guid FromId, Guid ToId, string Subject, string Description, string CreateDate, TicketStatusViewModel Status);

public record UpsertTicket(Guid? Id, Guid ToUser, string Subject, string Description);

public record AddAttachments(Guid TicketId, IEnumerable<FileViewModel> Files);

public record UpsertTicketResponse(TicketActionStatus Status, TicketViewModel? Ticket);

public record TicketStatusViewModel(short Code, string Name);

public record TicketDetailViewModel(TicketViewModel? Ticket, UserViewModel? FromUser, UserViewModel? ToUser);

public record GetTicketDetail(TicketActionStatus Status, TicketDetailViewModel? Ticket);

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