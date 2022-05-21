using ChTi.Client.Model;

namespace ChTi.Client.Abstraction;

public interface ITicket : IAsyncDisposable
{
    Task<Ticket?> UpsertAsync(UpsertTicket upsert);

    Task<IEnumerable<Attechment>> AddAttechmentsAsync(IEnumerable<AddAttechment> attechments);
}
