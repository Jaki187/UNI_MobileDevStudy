using TicketApp.ViewModels;

namespace TicketApp.Views;
public partial class AddEventPage : ContentPage
{
    public AddEventPage(AddEventPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}