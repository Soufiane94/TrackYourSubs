using Microsoft.EntityFrameworkCore;
using TrackYourSubs.Data;
using TrackYourSubs.Models;

namespace TrackYourSubs.Services;

public sealed class SubscriptionRepository : ISubscriptionRepository
{
    public async Task<List<Subscription>> GetAllAsync()
    {
        using var db = new AppDbContext();
        return await db.Subscriptions
            .Where(s => !s.IsArchived)
            .OrderBy(s => s.NextBillingDate)
            .ToListAsync();
    }

    public async Task<Subscription?> GetAsync(int id)
    {
        using var db = new AppDbContext();
        return await db.Subscriptions.FindAsync(id);
    }

    public async Task<int> AddAsync(Subscription s)
    {
        using var db = new AppDbContext();
        db.Subscriptions.Add(s);
        await db.Database.EnsureCreatedAsync();
        await db.SaveChangesAsync();
        return s.Id;
    }

    public async Task UpdateAsync(Subscription s)
    {
        using var db = new AppDbContext();
        db.Subscriptions.Update(s);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        using var db = new AppDbContext();
        var s = await db.Subscriptions.FindAsync(id);
        if (s is null) return;
        db.Subscriptions.Remove(s);
        await db.SaveChangesAsync();
    }
}
