using TicketApp.ViewModels;

namespace TicketApp.Views;

public partial class MainPage : ContentPage
{
    private readonly MainPageVm _viewModel;
    public MainPage(MainPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (_viewModel.LoadEventsCommand.CanExecute(null))
        {
            await _viewModel.LoadEventsCommand.ExecuteAsync(null);
        }
    }
}