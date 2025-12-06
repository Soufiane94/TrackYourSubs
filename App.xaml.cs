namespace TrackYourSubs;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        using var db = new Data.AppDbContext();
        db.Database.EnsureCreated();
        MainPage = new AppShell();
    }



}