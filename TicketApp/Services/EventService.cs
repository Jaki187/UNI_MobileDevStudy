using TicketApp.Models;
using SQLite;

namespace TicketApp.Services;

public class EventService
{
    SQLiteAsyncConnection _database;

    public interface IEventService
    {
        Task<List<Event>> GetAllEventsAsync();
        Task AddEventAsync(Event newEvent);
        Task DeleteEventAsync(int eventId);
        Task<Event> GetEventByIdAsync(int eventId);

    }
    
    
    private async Task Init()
    {
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        
        await _database.CreateTableAsync<Event>();
        await _database.CreateTableAsync<Ticket>();
        
    }
    
    
    public async Task<List<Event>> GetAllEventsAsync()
    {
        await Init();
        return await _database.Table<Event>().ToListAsync();
    }

    public async Task AddEventAsync(Event newEvent)
    {
        await Init();
        await _database.InsertAsync(newEvent);
    }

    public async Task DeleteEventAsync(int eventId)
    {
        await Init();
        await _database.DeleteAsync<Event>(eventId);
    }

    public async Task<Event> GetEventByIdAsync(int eventId)
    {
        await Init();
        return await _database.Table<Event>().Where(x => x.Id == eventId).FirstOrDefaultAsync();
    }
}