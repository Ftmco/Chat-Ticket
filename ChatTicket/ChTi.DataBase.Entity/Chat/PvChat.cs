namespace ChTi.DataBase.Entity;

public record PvChat
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ChatBaseId { get; set; }

    [Required]
    public Guid StarterUserId { get; set; }

    [Required]
    public Guid OppsiteUserId { get; set; }

    //Relationships
    //Navigation Property

    public virtual ChatBase ChatBase { get; set; }
}
