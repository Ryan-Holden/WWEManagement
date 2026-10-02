using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Data;
using WWEManagement.Data.Models;

namespace WWEManagement.Data.Repositories;

public class EfEventRepository : IEventRepository
{
    private readonly WWEManagementDbContext _context;

    public EfEventRepository(
        WWEManagementDbContext context)
    {
        _context = context;
    }

    public List<Event> GetAll()
    {
        return _context.Events
            .AsNoTracking()
            .OrderBy(e => e.EventDate)
            .ToList();
    }

    public Event? GetById(int id)
    {
        return _context.Events
            .AsNoTracking()
            .FirstOrDefault(e => e.EventId == id);
    }

    public List<Event> GetUpcoming(
        DateTime currentDate)
    {
        return _context.Events
            .AsNoTracking()
            .Where(e =>
                e.Status == "Scheduled"
                && e.EventDate >= currentDate)
            .OrderBy(e => e.EventDate)
            .ToList();
    }

    public int Add(Event wrestlingEvent)
    {
        _context.Events.Add(wrestlingEvent);

        _context.SaveChanges();

        return wrestlingEvent.EventId;
    }

    public bool Update(Event wrestlingEvent)
    {
        Event? existing =
            _context.Events
                .FirstOrDefault(
                    e => e.EventId == wrestlingEvent.EventId);

        if (existing == null)
        {
            return false;
        }

        existing.EventName =
            wrestlingEvent.EventName;

        existing.EventDate =
            wrestlingEvent.EventDate;

        existing.Venue =
            wrestlingEvent.Venue;

        existing.City =
            wrestlingEvent.City;

        existing.State =
            wrestlingEvent.State;

        existing.Status =
            wrestlingEvent.Status;

        _context.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        Event? wrestlingEvent =
            _context.Events
                .FirstOrDefault(
                    e => e.EventId == id);

        if (wrestlingEvent == null)
        {
            return false;
        }

        _context.Events.Remove(wrestlingEvent);

        _context.SaveChanges();

        return true;
    }
}