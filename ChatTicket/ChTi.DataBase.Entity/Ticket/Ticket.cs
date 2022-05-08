﻿using MongoDB.Bson.Serialization.Attributes;

namespace ChTi.DataBase.Entity;

public record Ticket
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("subject")]
    public string Subject { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("fromUserId")]
    public Guid FromUserId { get; set; }

    [BsonElement("toUserId")]
    public Guid ToUserId { get; set; }

    [BsonElement("createDate")]
    public DateTime CreateDate { get; set; }
}