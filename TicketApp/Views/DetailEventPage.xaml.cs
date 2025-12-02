using TicketApp.ViewModels;

namespace TicketApp.Views;

public partial class DetailEventPage : ContentPage
{
    public DetailEventPage(DetailEventPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}