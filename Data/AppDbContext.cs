using Microsoft.EntityFrameworkCore;
using TrackYourSubs.Models;

namespace TrackYourSubs.Data;

public sealed class AppDbContext : DbContext
{
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    private readonly string _dbPath;

    public AppDbContext()
    {
        var dir = FileSystem.AppDataDirectory;
        Directory.CreateDirectory(dir);
        _dbPath = Path.Combine(dir, "trackyoursubs.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}");

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var e in ChangeTracker.Entries<Subscription>())
            if (e.State is EntityState.Modified) e.Entity.UpdatedUtc = DateTime.UtcNow;
        return base.SaveChangesAsync(cancellationToken);
    }
}
