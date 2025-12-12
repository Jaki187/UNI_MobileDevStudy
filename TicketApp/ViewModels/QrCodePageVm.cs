using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using TicketApp.Services;

namespace TicketApp.ViewModels
{
    public class QrCodePageVm : INotifyPropertyChanged
    {
        private readonly QrCodeService _qrService = new();

        private ImageSource _qrCodeImageSource;
        public ImageSource QrCodeImageSource
        {
            get => _qrCodeImageSource;
            set { _qrCodeImageSource = value; OnPropertyChanged(); }
        }

        private string _guestName;
        public string GuestName
        {
            get => _guestName;
            set { _guestName = value; OnPropertyChanged(); }
        }

        private string _ticketCode;
        public string TicketCode
        {
            get => _ticketCode;
            set { _ticketCode = value; OnPropertyChanged(); }
        }

        public QrCodePageVm(string guestName, string ticketCode)
        {
            GuestName = guestName;
            TicketCode = ticketCode;

            // QR-Code generieren und Property setzen
            GenerateQrCode(ticketCode);
        }

        private void GenerateQrCode(string content)
        {
            QrCodeImageSource = _qrService.GenerateQrCode(content, 400);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}