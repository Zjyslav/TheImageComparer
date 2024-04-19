using Microsoft.EntityFrameworkCore;
using TheImageComparer.Logic.Models;

namespace TheImageComparer.Logic.Data;
public class ImageComparerDbContext : DbContext
{
    private readonly string _dbFilePath;
    public DbSet<ImageModel> Images { get; set; }
    public DbSet<VoteModel> Votes { get; set; }
    public ImageComparerDbContext(string dbFilePath)
    {
        _dbFilePath = dbFilePath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbFilePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImageModel>()
            .HasMany(i => i.VotesFor)
            .WithOne(v => v.VotedFor);

        modelBuilder.Entity<ImageModel>()
            .HasMany(i => i.VotesAgainst)
            .WithOne(v => v.VotedAgainst);

        base.OnModelCreating(modelBuilder);
    }
}
