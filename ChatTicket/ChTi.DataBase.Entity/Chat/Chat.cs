namespace ChTi.DataBase.Entity;

public record Chat : ChatBase
{
 
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public short Type { get; set; }
}
