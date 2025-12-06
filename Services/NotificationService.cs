// Services/NotificationService.cs
using Plugin.LocalNotification;

namespace TrackYourSubs.Services;

public sealed class NotificationService : INotificationService
{
    public Task ScheduleSubscriptionReminderAsync(int subscriptionId, string title, string body, DateTime when)
    {
        var request = new NotificationRequest
        {
            NotificationId = subscriptionId,
            Title = title,
            Description = body,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = when.ToLocalTime(),
                NotifyAutoCancelTime = when.ToLocalTime().AddHours(24)
            }
        };

        // Show() returns bool; ignore result and return a completed Task
        LocalNotificationCenter.Current.Show(request);
        return Task.CompletedTask;
    }

    public Task CancelSubscriptionReminderAsync(int subscriptionId)
    {
        // Cancel() returns bool; ignore result and return a completed Task
        LocalNotificationCenter.Current.Cancel(subscriptionId);
        return Task.CompletedTask;
    }
}
