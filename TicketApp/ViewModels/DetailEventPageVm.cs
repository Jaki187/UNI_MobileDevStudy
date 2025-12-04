using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using TicketApp.Models;
using TicketApp.Views;
using TicketApp.Services;

namespace TicketApp.ViewModels
{
    [QueryProperty(nameof(Event), "Event")]
    public partial class DetailEventPageVm : ObservableObject
    {
        private readonly TicketService _ticketService;
        
        [ObservableProperty] private Event _event;
        
        public ObservableCollection<Ticket> Tickets { get; } = new();

        public DetailEventPageVm(TicketService ticketService)
        {
            _ticketService = ticketService;
        }
        
        partial void OnEventChanged(Event value)
        {
            if (value != null)
            {
                LoadTicketsCommand.ExecuteAsync(null);
            }
        }

        [RelayCommand]
        public async Task LoadTicketsAsync()
        {
            if (Event == null) return;

            var ticketList = await _ticketService.GetTicketsForEventAsync(Event.Id);

            Tickets.Clear();
            foreach (var t in ticketList)
            {
                Tickets.Add(t);
            }
        }

        [RelayCommand]
        public async Task AddGuestAsync()
        {
            if (Event == null) return;

            // Wir erstellen das Pop-up und übergeben die EventId an das ViewModel
            // Annahme: Dein TicketService muss über DI bezogen werden (empfohlen)
            var ticketService = Shell.Current.Handler.MauiContext.Services.GetService<TicketService>();
    
            // 1. Instanzierung von ViewModel und Popup
            var popupVm = new AddTicketPopupVm(ticketService, Event.Id); 
            var popup = new AddTicketPopup { BindingContext = popupVm };

            // 2. Zeige das Pop-up und warte auf das Ergebnis
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
    
            // 3. Ergebnis verarbeiten
            if (result is Ticket newTicket && newTicket.SecretCode != null)
            {
                // Ticket wurde im Pop-up-ViewModel generiert und zurückgegeben
                await _ticketService.AddTicketAsync(newTicket);
                Tickets.Add(newTicket);
            }
        }
        
        
        [RelayCommand]
        public async Task DeleteTicketAsync(Ticket ticket)
        {
            if (ticket == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Delete?",
                $"Do you really want to delete the ticket for {ticket.GuestName}?", "Confirm", "Cancel");
            if (!confirm) return;
            
            await _ticketService.DeleteTicketAsync(ticket.Id);
            
            Tickets.Remove(ticket);
        }
    }
}