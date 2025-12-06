using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrackYourSubs.Services;
using TrackYourSubs.Models;

namespace TrackYourSubs.ViewModels;
    
public partial class EditSubscriptionViewModel : ObservableObject
{
    private readonly ISubscriptionRepository repo;
    private readonly INotificationService notifier;

    [ObservableProperty] int id;
    [ObservableProperty] string name = "";
    [ObservableProperty] decimal amount;
    [ObservableProperty] string currency = "EUR";
    [ObservableProperty] BillingFrequency frequency = BillingFrequency.Month;
    [ObservableProperty] int intervalCount = 1;
    [ObservableProperty] DateTime nextBillingDate = DateTime.Today.AddDays(1);
    [ObservableProperty] string? notes;

    public EditSubscriptionViewModel(ISubscriptionRepository repo, INotificationService notifier)
    {
        this.repo = repo;
        this.notifier = notifier;
    }

    public async Task LoadAsync(int id)
    {
        if (id <= 0) return;
        var s = await repo.GetAsync(id);
        if (s is null) return;
        Id = s.Id;
        Name = s.Name;
        Amount = s.Amount;
        Currency = s.Currency;
        Frequency = s.Frequency;
        IntervalCount = s.IntervalCount;
        NextBillingDate = s.NextBillingDate;
        Notes = s.Notes;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name) || Amount <= 0)
        {
            await Application.Current!.MainPage!.DisplayAlert("Invalid", "Name and positive amount required.", "OK");
            return;
        }

        var entity = new Subscription
        {
            Id = Id,
            Name = Name.Trim(),
            Amount = Amount,
            Currency = Currency,
            Frequency = Frequency,
            IntervalCount = IntervalCount < 1 ? 1 : IntervalCount,
            NextBillingDate = NextBillingDate.Date,
            Notes = Notes
        };

        if (Id == 0)
        {
            var newId = await repo.AddAsync(entity);
            Id = newId;
        }
        else
        {
            await repo.UpdateAsync(entity);
            await notifier.CancelSubscriptionReminderAsync(entity.Id);
        }

        // schedule reminder 2 days before billing if possible
        var remindAt = entity.NextBillingDate.AddDays(-2);
        if (remindAt > DateTime.Today)
        {
            var title = $"{entity.Name} renewal";
            var body  = $"{entity.Amount:F2} {entity.Currency} due on {entity.NextBillingDate:yyyy-MM-dd}";
            await notifier.ScheduleSubscriptionReminderAsync(entity.Id, title, body, remindAt);
        }

        await Shell.Current.GoToAsync(".."); // back
    }

    [RelayCommand]
    private Task DeleteAsync() => DeleteInternalAsync();

    private async Task DeleteInternalAsync()
    {
        if (Id == 0) { await Shell.Current.GoToAsync(".."); return; }
        var confirm = await Application.Current!.MainPage!.DisplayAlert("Delete", "Delete this subscription?", "Yes", "No");
        if (!confirm) return;

        await repo.DeleteAsync(Id);
        await notifier.CancelSubscriptionReminderAsync(Id);
        await Shell.Current.GoToAsync("..");
    }
}
