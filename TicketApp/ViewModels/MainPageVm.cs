using System.Collections.ObjectModel; // Wichtig für die Liste
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketApp.Models;
using TicketApp.Services;
using TicketApp.Views;

namespace TicketApp.ViewModels
{
    public partial class MainPageVm : ObservableObject
    {
        private readonly EventService _eventService;
        
        public ObservableCollection<Event> Events { get; } = new();
        
        public MainPageVm(EventService eventService)
        {
            _eventService = eventService;
        }
        
        [RelayCommand]
        public async Task LoadEventsAsync()
        {
            var eventsFromDb = await _eventService.GetAllEventsAsync();
            
            Events.Clear();
            foreach (var e in eventsFromDb)
            {
                Events.Add(e);
            }
        }
        
        [RelayCommand]
        public async Task NavigateToAddEventPageAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddEventPage));
        }
    }
}