using WWEManagement.Data.Models;
using WWEManagement.Data.Repositories;
using WWEManagement.Web.Dtos;
using WWEManagement.Web.ViewModels;

namespace WWEManagement.Web.Services;

public class WrestlerService : IWrestlerService
{
    private readonly IWrestlerRepository _wrestlerRepository;

    public WrestlerService(
        IWrestlerRepository wrestlerRepository)
    {
        _wrestlerRepository = wrestlerRepository;
    }

    public List<Wrestler> GetAll()
    {
        return _wrestlerRepository.GetAll();
    }

    public List<WrestlerDetails> GetAllDetails()
    {
        return _wrestlerRepository
            .GetWrestlerDetails();
    }

    public WrestlerDetails? GetById(int id)
    {
        return _wrestlerRepository
            .GetDetailsById(id);
    }

    public int Create(
        CreateWrestlerViewModel model)
    {
        Employee employee =
            CreateEmployee(
                model.FirstName,
                model.LastName,
                model.HireDate,
                model.IsActive);

        Wrestler wrestler =
            CreateWrestler(
                model.RingName,
                model.WeightClass,
                model.DebutDate,
                model.IsActive);

        return _wrestlerRepository
            .CreateWithEmployee(
                employee,
                wrestler);
    }

    public int Create(
        WrestlerRequestDto request)
    {
        Employee employee =
            CreateEmployee(
                request.FirstName,
                request.LastName,
                request.HireDate,
                request.IsActive);

        Wrestler wrestler =
            CreateWrestler(
                request.RingName,
                request.WeightClass,
                request.DebutDate,
                request.IsActive);

        return _wrestlerRepository
            .CreateWithEmployee(
                employee,
                wrestler);
    }

    public bool Update(
        EditWrestlerViewModel model)
    {
        WrestlerDetails? existing =
            GetById(model.WrestlerId);

        if (existing == null)
        {
            return false;
        }

        Employee employee =
            CreateUpdatedEmployee(
                existing,
                model.FirstName,
                model.LastName,
                model.HireDate,
                model.IsActive);

        Wrestler wrestler =
            CreateUpdatedWrestler(
                existing,
                model.RingName,
                model.WeightClass,
                model.DebutDate,
                model.IsActive);

        return _wrestlerRepository
            .UpdateWithEmployee(
                existing.WrestlerId,
                employee,
                wrestler);
    }

    public bool Update(
        int id,
        WrestlerRequestDto request)
    {
        WrestlerDetails? existing =
            GetById(id);

        if (existing == null)
        {
            return false;
        }

        Employee employee =
            CreateUpdatedEmployee(
                existing,
                request.FirstName,
                request.LastName,
                request.HireDate,
                request.IsActive);

        Wrestler wrestler =
            CreateUpdatedWrestler(
                existing,
                request.RingName,
                request.WeightClass,
                request.DebutDate,
                request.IsActive);

        return _wrestlerRepository
            .UpdateWithEmployee(
                existing.WrestlerId,
                employee,
                wrestler);
    }

    public bool Delete(int id)
    {
        return _wrestlerRepository
            .DeleteWithEmployee(id);
    }

    private static Employee CreateEmployee(
        string firstName,
        string lastName,
        DateTime hireDate,
        bool isActive)
    {
        return new Employee
        {
            FirstName = firstName,
            LastName = lastName,
            JobTitle = "Wrestler",
            HireDate = hireDate,
            IsActive = isActive
        };
    }

    private static Wrestler CreateWrestler(
        string ringName,
        string? weightClass,
        DateTime? debutDate,
        bool isActive)
    {
        return new Wrestler
        {
            RingName = ringName,
            WeightClass = weightClass,
            DebutDate = debutDate,
            IsActive = isActive
        };
    }

    private static Employee CreateUpdatedEmployee(
        WrestlerDetails existing,
        string firstName,
        string lastName,
        DateTime hireDate,
        bool isActive)
    {
        return new Employee
        {
            EmployeeId = existing.EmployeeId,
            FirstName = firstName,
            LastName = lastName,
            JobTitle = existing.JobTitle,
            HireDate = hireDate,
            IsActive = isActive
        };
    }

    private static Wrestler CreateUpdatedWrestler(
        WrestlerDetails existing,
        string ringName,
        string? weightClass,
        DateTime? debutDate,
        bool isActive)
    {
        return new Wrestler
        {
            WrestlerId = existing.WrestlerId,
            EmployeeId = existing.EmployeeId,
            RingName = ringName,
            WeightClass = weightClass,
            DebutDate = debutDate,
            IsActive = isActive
        };
    }
}