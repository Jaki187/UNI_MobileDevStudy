using TicketApp.ViewModels;

namespace TicketApp.Views;

public partial class QrCodePage : ContentPage
{
    public QrCodePage(string guestName, string ticketCode)
    {
        InitializeComponent();

        // ViewModel instanziieren und als BindingContext setzen
        BindingContext = new QrCodePageVm(guestName, ticketCode);
    }
}