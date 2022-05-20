using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.Entity;

public record ChatsUsers
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonRequired,BsonElement("chatId")]
    public Guid ChatId { get; set; }

    [BsonRequired,BsonElement("userId")]
    public Guid UserId { get; set; }
}
