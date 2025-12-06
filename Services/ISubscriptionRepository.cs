using TrackYourSubs.Models;

namespace TrackYourSubs.Services;

public interface ISubscriptionRepository
{
    Task<List<Subscription>> GetAllAsync();
    Task<Subscription?> GetAsync(int id);
    Task<int> AddAsync(Subscription s);
    Task UpdateAsync(Subscription s);
    Task DeleteAsync(int id);
}
