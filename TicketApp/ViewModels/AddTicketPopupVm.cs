using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using TicketApp.Models;
using TicketApp.Services;

namespace TicketApp.ViewModels
{
    // Die Klasse muss von ObservableObject erben
    public partial class AddTicketPopupVm : ObservableObject
    {
        // HINWEIS: _ticketService wird hier nicht benötigt, da das Hinzufügen 
        // in DetailEventPageVm nach dem Schließen des Popups erfolgt. 
        // Wir behalten es aber, falls es für andere Logik genutzt wird.
        private readonly TicketService _ticketService;
        private readonly int _eventId; // Speichert die ID des aktuellen Events
        
        // Properties für die Eingabe des Benutzers
        [ObservableProperty]
        private string _guestName = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        // 💡 Korrigierter Konstruktor, der die 2 Parameter annimmt
        public AddTicketPopupVm(TicketService ticketService, int eventId)
        {
            // Speichere die übergebenen Abhängigkeiten/Daten
            _ticketService = ticketService;
            _eventId = eventId;
        }

        [RelayCommand]
        // 💡 Die Methode ist nicht async und akzeptiert 'object' für den XAML CommandParameter
        private void SaveTicket(object popupParameter)
        {
            // 1. Casten und Validieren des Popup-Parameters
            if (popupParameter is not Popup popupInstance) return;
            
            // 2. Einfache Validierung der Benutzereingabe
            if (string.IsNullOrWhiteSpace(GuestName))
            {
                // In einer echten App: Fehlermeldung anzeigen
                // Beispiel: Shell.Current.DisplayAlert("Fehler", "Bitte den Namen des Gastes eingeben.", "OK");
                return;
            }

            // 3. Erstellung des neuen Ticket-Objekts
            var newTicket = new Ticket
            {
                EventId = _eventId,
                GuestName = GuestName.Trim(),
                Email = Email.Trim(),
                IsUsed = false,
                // Generiere hier einen zufälligen SecretCode
                SecretCode = GenerateSecretCode() 
            };
            
            // 4. Gib das neue Ticket zurück und schließe das Popup!
            // 💡 Korrekte Verwendung der gecasteten Instanz 'popupInstance'
            popupInstance.Close(newTicket);
        }

        [RelayCommand]
        // 💡 Die Methode akzeptiert 'object' für den XAML CommandParameter
        private void Cancel(object popupParameter)
        {
            // 1. Casten und Validieren des Popup-Parameters
            if (popupParameter is not Popup popupInstance) return;
    
            // 2. Gib null zurück, um Abbruch zu signalisieren, und schließe das Popup.
            popupInstance.Close(null);
        }
        
        // Hilfsmethode zur Erzeugung eines eindeutigen Codes
        private string GenerateSecretCode()
        {
            // Erzeugt einen kurzen, zufälligen alphanumerischen Code
            return Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        }
    }
}