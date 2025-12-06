using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrackYourSubs.Models;
using TrackYourSubs.Services;

namespace TrackYourSubs.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    private readonly ISubscriptionRepository repo;

    public ObservableCollection<Subscription> Items { get; } = new();

    public DashboardViewModel(ISubscriptionRepository repo)
    {
        Title = "Subscriptions";
        this.repo = repo;
    }

       [ObservableProperty] decimal monthlyTotal;

    [RelayCommand]
    public async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            Items.Clear();
            var all = await repo.GetAllAsync();
            foreach (var s in all) Items.Add(s);
            MonthlyTotal = all.Sum(s => BillingCalculator.MonthlyizedCost(s.Amount, s.Frequency, s.IntervalCount));
        }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private Task AddAsync() => Shell.Current.GoToAsync(nameof(Views.EditSubscriptionPage));
    
}
