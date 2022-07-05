namespace ChTi.DataBase.Entity;

public record ChatBase
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    public DateTime UpdateDate { get; set; }

    [Required]
    public short Status { get; set; }
}
