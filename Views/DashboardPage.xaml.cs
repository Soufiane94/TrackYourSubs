using TrackYourSubs.ViewModels;

namespace TrackYourSubs.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        Loaded += async (_, __) => await vm.LoadAsync();
    }
    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if (e.CurrentSelection.FirstOrDefault() is TrackYourSubs.Models.Subscription s)
    {
        ((CollectionView)sender).SelectedItem = null;
        await Shell.Current.GoToAsync($"{nameof(EditSubscriptionPage)}?id={s.Id}");
    }
}
}
