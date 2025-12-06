using CommunityToolkit.Mvvm.ComponentModel;

namespace TrackYourSubs.ViewModels;
public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty] bool isBusy;
    [ObservableProperty] string title = "";
}
