using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using TicketApp.Services;
using CommunityToolkit.Maui;
using TicketApp.Views;
using TicketApp.ViewModels;

namespace TicketApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
            builder.Services.AddSingleton<EventService>();
            builder.Services.AddSingleton<TicketService>();
            
            builder.Services.AddSingleton<MainPageVm>();
            builder.Services.AddSingleton<MainPage>();
            
            builder.Services.AddSingleton<AddEventPageVm>();
            builder.Services.AddSingleton<AddEventPage>();
            
            builder.Services.AddSingleton<DetailEventPageVm>();
            builder.Services.AddSingleton<DetailEventPage>();

            builder.Services.AddTransient<AddTicketPopupVm>();
            builder.Services.AddTransient<AddTicketPopup>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}