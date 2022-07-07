namespace ChTi.DataBase.Entity;

[Index(nameof(StarterUserId), nameof(OppsiteUserId), IsUnique = true)]
public record PvChat : ChatBase
{
    [Required]
    public Guid StarterUserId { get; set; }

    [Required]
    public Guid OppsiteUserId { get; set; }
}
