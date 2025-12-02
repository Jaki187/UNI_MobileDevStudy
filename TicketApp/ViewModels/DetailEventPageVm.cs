using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketApp.Models;
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
            string guestName = await Shell.Current.DisplayPromptAsync("New Guest", "Guest name:");

            if (string.IsNullOrWhiteSpace(guestName)) return;

            var newTicket = new Ticket
            {
                EventId = Event.Id,
                GuestName = guestName,
                Code = Guid.NewGuid().ToString(), // Generiert den QR-Code Inhalt
                IsCheckedIn = false
            };
            
            await _ticketService.AddTicketAsync(newTicket);

            Tickets.Add(newTicket);
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