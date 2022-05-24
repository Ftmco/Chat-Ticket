namespace ChTi.DataBase.Entity;

public record Ticket
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Subject { get; set; }

    public string Description { get; set; }

    [Required]
    public Guid FromUserId { get; set; }

    [Required]
    public Guid ToUserId { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// Ticket Status 
    /// <see cref="TicketStatus"/>
    /// </summary>
    [Required]
    public short Status { get; set; }

    [Required]
    public Guid ChatId { get; set; }
}

public enum TicketStatus
{
    Open = 0,
    Close = 1,
    Deleted = 2
}
