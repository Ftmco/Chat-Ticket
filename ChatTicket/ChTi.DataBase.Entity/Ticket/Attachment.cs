using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.Entity;

public record Attachment
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("fileId")]
    public Guid FileId { get; set; }

    [BsonElement("fileToken")]
    public string FileToken { get; set; }

    [BsonElement("ticketId")]
    public Guid TicketId { get; set; }
}
