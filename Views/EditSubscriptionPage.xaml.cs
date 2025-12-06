using Microsoft.Maui.Controls;
using TrackYourSubs.ViewModels;
    
namespace TrackYourSubs.Views;

public partial class EditSubscriptionPage : ContentPage, IQueryAttributable
{
    private readonly EditSubscriptionViewModel vm;
    private int? itemId;

    public EditSubscriptionPage(EditSubscriptionViewModel vm)
    {
        InitializeComponent();
        BindingContext = this.vm = vm;
    }

    // Called by Shell when navigating with ?id=...
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var value))
        {
            if (value is string s && int.TryParse(s, out var id)) itemId = id;
            else if (value is int id2) itemId = id2;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (itemId is int id && id > 0)
            await vm.LoadAsync(id);
    }
}
