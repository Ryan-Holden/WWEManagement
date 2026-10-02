using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Data;
using WWEManagement.Data.Models;
using WWEManagement.Data.Repositories;

namespace WWEManagement.Tests.Repositories;

public class EfEventRepositoryTests
{
    private static WWEManagementDbContext CreateContext()
    {
        DbContextOptions<WWEManagementDbContext> options =
            new DbContextOptionsBuilder<WWEManagementDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString())
                .Options;

        return new WWEManagementDbContext(options);
    }

    [Fact]
    public void GetAll_ReturnsEventsOrderedByDate()
    {
        using WWEManagementDbContext context =
            CreateContext();

        context.Events.AddRange(
            new Event
            {
                EventId = 1,
                EventName = "Later Event",
                EventDate = new DateTime(2026, 12, 20),
                Venue = "Arena B",
                City = "Chicago",
                State = "Illinois",
                Status = "Scheduled"
            },
            new Event
            {
                EventId = 2,
                EventName = "Earlier Event",
                EventDate = new DateTime(2026, 11, 10),
                Venue = "Arena A",
                City = "Chicago",
                State = "Illinois",
                Status = "Scheduled"
            });

        context.SaveChanges();

        EfEventRepository repository =
            new EfEventRepository(context);

        List<Event> result =
            repository.GetAll();

        Assert.Equal(2, result.Count);
        Assert.Equal(
            "Earlier Event",
            result[0].EventName);
        Assert.Equal(
            "Later Event",
            result[1].EventName);
    }

    [Fact]
    public void GetUpcoming_ReturnsOnlyFutureScheduledEvents()
    {
        using WWEManagementDbContext context =
            CreateContext();

        DateTime currentDate =
            new DateTime(2026, 10, 1);

        context.Events.AddRange(
            new Event
            {
                EventId = 1,
                EventName = "Future Scheduled",
                EventDate = new DateTime(2026, 11, 1),
                Venue = "Arena",
                City = "Chicago",
                Status = "Scheduled"
            },
            new Event
            {
                EventId = 2,
                EventName = "Future Completed",
                EventDate = new DateTime(2026, 11, 2),
                Venue = "Arena",
                City = "Chicago",
                Status = "Completed"
            },
            new Event
            {
                EventId = 3,
                EventName = "Past Scheduled",
                EventDate = new DateTime(2026, 9, 1),
                Venue = "Arena",
                City = "Chicago",
                Status = "Scheduled"
            });

        context.SaveChanges();

        EfEventRepository repository =
            new EfEventRepository(context);

        List<Event> result =
            repository.GetUpcoming(
                currentDate);

        Assert.Single(result);
        Assert.Equal(
            "Future Scheduled",
            result[0].EventName);
    }

    [Fact]
    public void Add_SavesEventAndReturnsGeneratedId()
    {
        using WWEManagementDbContext context =
            CreateContext();

        EfEventRepository repository =
            new EfEventRepository(context);

        Event wrestlingEvent =
            new Event
            {
                EventName = "Test Event",
                EventDate = new DateTime(2026, 12, 1),
                Venue = "Test Arena",
                City = "Chicago",
                State = "Illinois",
                Status = "Scheduled"
            };

        int id =
            repository.Add(wrestlingEvent);

        Assert.True(id > 0);

        Event? saved =
            context.Events
                .FirstOrDefault(
                    e => e.EventId == id);

        Assert.NotNull(saved);
        Assert.Equal(
            "Test Event",
            saved.EventName);
    }

    [Fact]
    public void Delete_RemovesExistingEvent()
    {
        using WWEManagementDbContext context =
            CreateContext();

        Event wrestlingEvent =
            new Event
            {
                EventId = 1,
                EventName = "Delete Me",
                EventDate = new DateTime(2026, 12, 1),
                Venue = "Test Arena",
                City = "Chicago",
                Status = "Scheduled"
            };

        context.Events.Add(
            wrestlingEvent);

        context.SaveChanges();

        EfEventRepository repository =
            new EfEventRepository(context);

        bool result =
            repository.Delete(1);

        Assert.True(result);
        Assert.Empty(context.Events);
    }
}