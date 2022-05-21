namespace ChTi.DataBase.Entity;

public record Attachment
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonRequired, BsonElement("fileId")]
    public Guid FileId { get; set; }

    [BsonRequired, BsonElement("fileToken")]
    public string FileToken { get; set; }

    [BsonRequired, BsonElement("objectId")]
    public Guid ObjectId { get; set; }
}
