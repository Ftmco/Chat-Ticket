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

    /// <summary>
    /// Ticket Status 
    /// <see cref="TicketStatus"/>
    /// </summary>
    [BsonElement("status")]
    public short Status { get; set; }
}

public enum TicketStatus
{
    Open = 0,
    Close = 1,
    Deleted = 2
}
