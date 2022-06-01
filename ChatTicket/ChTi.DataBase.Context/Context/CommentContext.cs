using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.Context;

public class CommentContext : DbContext
{
    public CommentContext(DbContextOptions<CommentContext> options) : base(options)
    {

    }

    public CommentContext()
    {

    }

    public static string ConnectionString { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(ConnectionString);
    }

    public virtual Comment Comment { get; set; }
}
