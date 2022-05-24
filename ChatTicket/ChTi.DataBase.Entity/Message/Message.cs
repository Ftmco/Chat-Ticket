namespace ChTi.DataBase.Entity;

public record Message
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ChatId { get; set; }
       
    public string Text { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    public long MessageId { get; set; }

    public long ReplyMessageId { get; set; }
}
