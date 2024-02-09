using Borealis2tsx.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Borealis2tsx.Server.DataDBContext;

public class DataLineDbContext : DbContext
{
    public DataLineDbContext(DbContextOptions<DataLineDbContext> options)
        : base(options)
    {
    }

    public DbSet<DataLine> DataLine { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataLine>().HasNoKey();
    }
}