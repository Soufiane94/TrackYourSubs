namespace TrackYourSubs.Services;

public interface INotificationService
{
    Task ScheduleSubscriptionReminderAsync(int subscriptionId, string title, string body, DateTime when);
    Task CancelSubscriptionReminderAsync(int subscriptionId);
}
