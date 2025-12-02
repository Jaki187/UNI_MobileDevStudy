using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SQLite;


namespace TicketApp.Models;

public partial class Ticket: ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [Indexed] 
    public int EventId { get; set; }
    
    public string GuestName { get; set; }
        
    public string Email { get; set; }
  
    public string Code { get; set; } 
    
    public bool IsCheckedIn { get; set; }  
        
    public DateTime? CheckInTime { get; set; }
}