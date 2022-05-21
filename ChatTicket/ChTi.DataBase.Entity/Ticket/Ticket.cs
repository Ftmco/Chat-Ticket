namespace ChTi.DataBase.Entity;

public record Ticket
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("subject"), BsonRequired]
    public string Subject { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("fromUserId"), BsonRequired]
    public Guid FromUserId { get; set; }

    [BsonElement("toUserId"), BsonRequired]
    public Guid ToUserId { get; set; }

    [BsonElement("createDate"), BsonRequired]
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// Ticket Status 
    /// <see cref="TicketStatus"/>
    /// </summary>
    [BsonElement("status"), BsonRequired]
    public short Status { get; set; }

    [BsonElement("chatId")]
    public Guid ChatId { get; set; }
}

public enum TicketStatus
{
    Open = 0,
    Close = 1,
    Deleted = 2
}
