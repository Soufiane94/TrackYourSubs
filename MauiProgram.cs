using Microsoft.Extensions.Logging;
using TrackYourSubs.Services;
using TrackYourSubs.ViewModels;

namespace TrackYourSubs;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
	builder.Services.AddSingleton<Services.ISubscriptionRepository, Services.SubscriptionRepository>();
	builder.Services.AddTransient<ViewModels.DashboardViewModel>();
	builder.Services.AddTransient<Views.DashboardPage>();
	builder.Services.AddSingleton<Services.INotificationService, Services.NotificationService>();
	builder.Services.AddTransient<ViewModels.EditSubscriptionViewModel>();
	builder.Services.AddTransient<Views.EditSubscriptionPage>();



#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
