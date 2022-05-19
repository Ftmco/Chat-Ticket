using ChTi.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Client.Abstraction;

public interface ITicket : IAsyncDisposable
{
    Task<Ticket?> UpsertAsync(UpsertTicket upsert);

    Task<IEnumerable<Attechment>> AddAttechmentsAsync(IEnumerable<AddAttechment> attechments);
}
