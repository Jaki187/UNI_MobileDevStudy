using TicketApp.Models;
using SQLite;

namespace TicketApp.Services;

public class EventService
{
    SQLiteAsyncConnection database;

    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        
    }
    
    
    private async Task Init()
    {
        if (database is not null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await database.CreateTableAsync<Event>();
    }
    
    
    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        await Init();
        return await database.Table<Event>().ToListAsync();
    }
}