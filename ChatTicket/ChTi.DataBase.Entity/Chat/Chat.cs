namespace ChTi.DataBase.Entity;

public record Chat
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("token"), BsonRequired]
    public string Token { get; set; }

    [BsonElement("name"), BsonRequired]
    public string Name { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("createDate"), BsonRequired]
    public DateTime CreateDate { get; set; }

    [BsonElement("status"), BsonRequired]
    public short Status { get; set; }

    [BsonElement("type"), BsonRequired]
    public short Type { get; set; }
}
