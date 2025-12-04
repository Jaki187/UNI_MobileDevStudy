using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketApp.Models;
using TicketApp.Services;

namespace TicketApp.ViewModels
{
    public partial class AddEventPageVm : ObservableObject
    {
        private readonly EventService _eventService;

        public AddEventPageVm(EventService eventService)
        {
            _eventService = eventService;
            
            EventDate = DateTime.Now;
            EventTime = DateTime.Now.TimeOfDay;
        }

       
        [ObservableProperty] private string _name;
        [ObservableProperty] private string _location;
        [ObservableProperty] private string _description;
        [ObservableProperty] private DateTime _eventDate;
        [ObservableProperty] private TimeSpan _eventTime;
        
        
        [RelayCommand]
        public async Task SaveEventAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Location)) return;

            var newEvent = new Event
            {
                Name = Name,
                Location = Location,
                Description = Description,
                Date = EventDate.Date + EventTime
            };

            await _eventService.AddEventAsync(newEvent);
            
            ClearForm();
            
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
            ClearForm();
        }
        
        
        private void ClearForm()
        {
            Name = string.Empty;
            Location = string.Empty;
            Description = string.Empty;
            EventDate = DateTime.Now;
            EventTime = DateTime.Now.TimeOfDay;
        }
    }
}