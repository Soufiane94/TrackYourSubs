namespace TrackYourSubs;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.EditSubscriptionPage), typeof(Views.EditSubscriptionPage));

    }
}