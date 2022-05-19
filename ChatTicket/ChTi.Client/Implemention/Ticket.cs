using ChTi.Client.Abstraction;
using ChTi.Client.Model;
using ChTi.Web.Grpc;
using TicketClient = ChTi.Web.Grpc.Ticket.TicketClient;

namespace ChTi.Client.Implemention;

public class Ticket : ITicket
{
    readonly IGrpcRule _gRPC;

    public Ticket(IGrpcRule gRPC)
    {
        _gRPC = gRPC;
    }

    public async Task<IEnumerable<Attechment>> AddAttechmentsAsync(IEnumerable<AddAttechment> attechments)
    {
        var channel = await _gRPC.OpenChannelAsync();
        TicketClient client = new(channel);
        var request = client.AddAttechments();
        foreach (var attechment in attechments)
            await request.RequestStream.WriteAsync(new()
            {
                Base64 = attechment.Base64,
                Name = attechment.Name,
            });
        await request.RequestStream.CompleteAsync();
        AddAttechmentReply? response = await request.ResponseAsync;
        return response.Attechments.Select((attechment) =>
        {
            _ = Guid.TryParse(attechment.Id, out Guid id);
            return new Attechment(Id: id, FileToken: attechment.FileToken, Name: attechment.Name);
        });
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _gRPC.DisposeAsync();
    }

    public async Task<Model.Ticket?> UpsertAsync(UpsertTicket upsert)
    {
        var channel = await _gRPC.OpenChannelAsync();
        TicketClient client = new(channel);
        TicketReply? request = await client.UpsertTicketAsync(new()
        {
            FromUser = upsert.FromUser.ToString(),
            Subject = upsert.Subject,
            Text = upsert.Text,
            ToUser = upsert.ToUser.ToString()
        });
        if (request != null)
        {
            _ = Guid.TryParse(request.TicketId, out Guid ticketId);
            return new Model.Ticket(Id: ticketId, Subject: request.Subject, Text: request.Text);
        }
        return null;
    }
}
