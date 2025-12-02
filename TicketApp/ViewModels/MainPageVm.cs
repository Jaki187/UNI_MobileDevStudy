using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketApp.Models;
using TicketApp.Services;

namespace TicketApp.ViewModels
{
    public partial class MainPageVm : ObservableObject
    {
        public readonly EventService _eventService;

        public MainPageVm(EventService eventService)
        {
            _eventService = eventService;
        }
    }
}