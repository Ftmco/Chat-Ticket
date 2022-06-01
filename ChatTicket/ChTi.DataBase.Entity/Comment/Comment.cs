using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.Entity;

public record Comment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Text { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ObjectId { get; set; }

    public Guid? ReplyId { get; set; }

    //Navigation Property
    //Relationships

    public virtual ICollection<Comment> Comments { get; set; }
}