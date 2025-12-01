using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace TicketApp.Models;

public partial class Event: ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
}