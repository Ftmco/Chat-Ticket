namespace ChTi.DataBase.Entity;

public record Attachment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid FileId { get; set; }

    [Required]
    public string FileToken { get; set; }

    [Required]
    public Guid ObjectId { get; set; }
}
