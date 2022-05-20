namespace ChTi.DataBase.Entity;

public record Chat
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonRequired]
    [BsonElement("token")]
    public string Token { get; set; }

    [BsonRequired]
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonRequired]
    [BsonElement("createDate")]
    public DateTime CreateDate { get; set; }

    [BsonRequired]
    [BsonElement("status")]
    public short Status { get; set; }

    [BsonRequired]
    [BsonElement("type")]
    public short Type { get; set; }
}
