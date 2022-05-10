using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.Entity;

public record Comment
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("userId")]
    public Guid UserId { get; set; }

    [BsonElement("replyId")]
    public Guid? ReplyId { get; set; }

    [BsonElement("objectId")]
    public string ObjectId { get; set; }

    [BsonElement("text")]
    public string Text { get; set; }

    [BsonElement("isActive")]
    public bool IsActive { get; set; }

    [BsonElement("createDate")]
    public DateTime CreateDate { get; set; }
}
