﻿namespace ChTi.DataBase.Entity;

public record Chat
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ChatBaseId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    //Relationships
    //Navigation Property

    public virtual ChatBase ChatBase { get; set; }
}
