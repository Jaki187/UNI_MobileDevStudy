namespace TicketApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.AddEventPage), typeof(Views.AddEventPage));
        Routing.RegisterRoute(nameof(Views.DetailEventPage), typeof(Views.DetailEventPage));
    }
}