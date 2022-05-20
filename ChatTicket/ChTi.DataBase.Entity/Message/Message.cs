namespace ChTi.DataBase.Entity;

public record Message
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonRequired, BsonElement("userId")]
    public Guid UserId { get; set; }

    [BsonRequired, BsonElement("chatId")]
    public Guid ChatId { get; set; }

    [BsonElement("text")]
    public string Text { get; set; }

    [BsonRequired, BsonElement("createDate")]
    public DateTime CreateDate { get; set; }
}
