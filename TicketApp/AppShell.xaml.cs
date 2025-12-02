namespace TicketApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.AddEventPage), typeof(Views.AddEventPage));
    }
}