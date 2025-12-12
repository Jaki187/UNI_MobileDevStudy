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
        private readonly IEventService _eventService;
        
        public ObservableCollection<Event> Events { get; } = new();
        
        public MainPageVm(IEventService eventService)
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
        public async Task GoToDetailsAsync(Event selectedEvent)
        {
            if (selectedEvent == null) return;

            var navParam = new Dictionary<string, object>
            {
                { "Event", selectedEvent }
            };
            await Shell.Current.GoToAsync(nameof(DetailEventPage), navParam);
        }
        
        [RelayCommand]
        public async Task NavigateToAddEventPageAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddEventPage));
        }
        
        [RelayCommand]
        public async Task DeleteEventAsync(Event eventToDelete)
        {
            if (eventToDelete == null) return;

            // Sicherheitsfrage
            bool answer = await Shell.Current.DisplayAlert("Delete?", $"Do you want to delete '{eventToDelete.Name}' ?", "Confirm", "Cancel");
    
            if (answer)
            {
                await _eventService.DeleteEventAsync(eventToDelete.Id);

                Events.Remove(eventToDelete);
            }
        }
    }
}