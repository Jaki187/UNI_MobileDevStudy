using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TicketApp.Models;
using TicketApp.Services;

public partial class MainPageVm : ObservableObject
{
    public readonly EventService _eventService;
    
    public MainPageVm(EventService eventService)
    {
        _eventService = eventService;
    }
}