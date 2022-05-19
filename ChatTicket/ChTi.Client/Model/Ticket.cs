using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Client.Model;

public record UpsertTicket(Guid FromUser, Guid ToUser, string Subject, string Text);

public record Ticket(Guid Id,string Subject,string Text);

public record AddAttechment(string Base64,string Name);

public record Attechment(Guid Id,string FileToken,string Name);