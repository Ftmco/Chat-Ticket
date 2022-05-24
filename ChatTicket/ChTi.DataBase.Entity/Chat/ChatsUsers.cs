namespace ChTi.DataBase.Entity;

public record ChatsUsers
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ChatId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public short Type { get; set; }

    [Required]
    public DateTime JoinDate { get; set; }
}
