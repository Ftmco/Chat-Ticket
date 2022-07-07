namespace ChTi.DataBase.Entity;

[Index(nameof(Link))]
public record ChannelChat : ChatBase
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public string Link { get; set; }

    [Required]
    public short Type { get; set; }
}
