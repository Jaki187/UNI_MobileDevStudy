using TicketApp.Models;
using SQLite;

namespace TicketApp.Services;

public class TicketService
{
    SQLiteAsyncConnection _database;
    
    public interface ITicketService
    {
        Task<List<Ticket>> GetTicketsForEventAsync(int eventId);
        Task AddTicketAsync(Ticket newTicket);
        Task DeleteTicketAsync(int ticketId);
        Task<Ticket> GetTicketByCodeAsync(string code);
        Task UpdateTicketStatusAsync(Ticket ticket);
        Task<int> GetGuestCountAsync(int eventId);
        
    }
    
    private async Task Init()
    {
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        
        await _database.CreateTableAsync<Ticket>();
        await _database.CreateTableAsync<Event>();
    }

    public async Task<List<Ticket>> GetTicketsForEventAsync(int eventId)
    {
        await Init();
        return await _database.Table<Ticket>().Where(x => x.EventId == eventId).ToListAsync();
    }

    public async Task AddTicketAsync(Ticket newTicket)
    {
        await Init();
        await _database.InsertAsync(newTicket);
    }

    public async Task DeleteTicketAsync(int ticketId)
    {
        await Init();
        await _database.DeleteAsync<Ticket>(ticketId);
    }

    public async Task<Ticket> GetTicketByCodeAsync(string code)
    {
        await Init();
        return await _database.Table<Ticket>().Where(x => x.SecretCode == code).FirstOrDefaultAsync();
    }

    public async Task UpdateTicketStatusAsync(Ticket ticket)
    {
        await Init();
        await _database.UpdateAsync(ticket);
    }

    public async Task<int> GetGuestCountAsync(int eventId)
    {
        await Init();
        return await _database.Table<Event>().Where(x => x.Id == eventId).CountAsync();
    }
}