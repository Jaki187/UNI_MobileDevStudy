using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace TicketApp.Models;

public partial class Ticket : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int EventId { get; set; }

    public string GuestName { get; set; }

    public string Email { get; set; }

    public string SecretCode { get; set; }
    public bool IsUsed { get; set; }

    public DateTime? UsedAtTime { get; set; }
}