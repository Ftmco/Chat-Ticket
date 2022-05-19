using Grpc.Core;

namespace ChTi.Web.Grpc.Services;

public class TicketService : Ticket.TicketBase
{
    public override Task<TicketReply> UpsertTicket(UpsertRequest request, ServerCallContext context)
    {
        return base.UpsertTicket(request, context);
    }

    public override Task<AddAttechmentReply> AddAttechments(IAsyncStreamReader<AddAttechmentRequest> requestStream, ServerCallContext context)
    {
        return base.AddAttechments(requestStream, context);
    }
}
