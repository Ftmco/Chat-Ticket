namespace ChTi.DataBase.Context;

public class TicketContext : DbContext
{
    public TicketContext(DbContextOptions<TicketContext> options) : base(options)
    { }

    public TicketContext()
    { }

    public static string ConnectionString { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(ConnectionString);
    }

    public virtual DbSet<Ticket> Ticket { get; set; }

    public virtual DbSet<Attachment> Attachment { get; set; }
}
