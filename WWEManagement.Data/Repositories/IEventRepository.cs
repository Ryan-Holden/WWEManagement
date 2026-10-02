using WWEManagement.Data.Models;

namespace WWEManagement.Data.Repositories;

public interface IEventRepository
{
    List<Event> GetAll();

    Event? GetById(int id);

    List<Event> GetUpcoming(
        DateTime currentDate);

    int Add(Event wrestlingEvent);

    bool Update(Event wrestlingEvent);

    bool Delete(int id);
}