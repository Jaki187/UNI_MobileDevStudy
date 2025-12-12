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
        
            builder.Services.AddSingleton<IEventService, EventService>();
            builder.Services.AddSingleton<ITicketService, TicketService>();
            
            builder.Services.AddSingleton<MainPageVm>();
            builder.Services.AddSingleton<MainPage>();
            
            builder.Services.AddSingleton<AddEventPageVm>();
            builder.Services.AddSingleton<AddEventPage>();
            
            builder.Services.AddSingleton<DetailEventPageVm>();
            builder.Services.AddSingleton<DetailEventPage>();

            builder.Services.AddTransient<AddTicketPopupVm>();
            builder.Services.AddTransient<AddTicketPopup>();
            
            builder.Services.AddSingleton<QrCodeService>();
            builder.Services.AddTransient<QrCodePageVm>(); 
            builder.Services.AddTransient<QrCodePage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}