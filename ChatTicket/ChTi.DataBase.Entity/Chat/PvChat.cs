namespace ChTi.DataBase.Entity;

public record PvChat : ChatBase
{
    [Required]
    public Guid StarterUserId { get; set; }

    [Required]
    public Guid OppsiteUserId { get; set; }
}
