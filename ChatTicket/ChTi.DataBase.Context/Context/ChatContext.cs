using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.Context;

public class ChatContext : DbContext
{
    public ChatContext(DbContextOptions<ChatContext> options) : base(options)
    { }

    public ChatContext()
    { }

    public static string ConnectionString { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(ConnectionString);
    }

    
    public virtual DbSet<PvChat> PvChat { get; set; } = null!;

    public virtual DbSet<Chat> Chat { get; set; } = null!;

    public virtual DbSet<ChatsUsers> ChatsUsers { get; set; } = null!;

    public virtual DbSet<Message> Message { get; set; } = null!;

    public virtual DbSet<Attachment> Attachment { get; set; } = null!;
}
