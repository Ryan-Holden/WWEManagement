using WWEManagement.Data.Models;

namespace WWEManagement.Data.Repositories;

public interface IWrestlerRepository
{
    List<Wrestler> GetAll();

    List<WrestlerDetails> GetWrestlerDetails();

    WrestlerDetails? GetDetailsById(int id);

    int CreateWithEmployee(
        Employee employee,
        Wrestler wrestler);

    bool UpdateWithEmployee(
        int wrestlerId,
        Employee employee,
        Wrestler wrestler);

    bool DeleteWithEmployee(int wrestlerId);
}