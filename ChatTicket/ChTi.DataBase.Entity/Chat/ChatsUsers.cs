namespace ChTi.DataBase.Entity;

public record ChatsUsers
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonRequired, BsonElement("chatId")]
    public Guid ChatId { get; set; }

    [BsonRequired, BsonElement("userId")]
    public Guid UserId { get; set; }

    [BsonRequired, BsonElement("type")]
    public short Type { get; set; }

    [BsonRequired,BsonElement("joinDate")]
    public DateTime JoinDate { get; set; }
}
