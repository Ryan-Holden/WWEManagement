using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Data;
using WWEManagement.Data.Models;

namespace WWEManagement.Data.Repositories;

public class EfWrestlerRepository : IWrestlerRepository
{
    private readonly WWEManagementDbContext _context;

    public EfWrestlerRepository(
        WWEManagementDbContext context)
    {
        _context = context;
    }

    public List<Wrestler> GetAll()
    {
        return _context.Wrestlers
            .AsNoTracking()
            .OrderBy(w => w.WrestlerId)
            .ToList();
    }

    public List<WrestlerDetails> GetWrestlerDetails()
    {
        return GetDetailsQuery()
            .OrderBy(w => w.WrestlerId)
            .ToList();
    }

    public WrestlerDetails? GetDetailsById(int id)
    {
        return GetDetailsQuery()
            .FirstOrDefault(
                w => w.WrestlerId == id);
    }

    public int CreateWithEmployee(
        Employee employee,
        Wrestler wrestler)
    {
        wrestler.Employee = employee;

        _context.Wrestlers.Add(wrestler);

        _context.SaveChanges();

        return wrestler.WrestlerId;
    }

    public bool UpdateWithEmployee(
        int wrestlerId,
        Employee employee,
        Wrestler wrestler)
    {
        Wrestler? existingWrestler =
            _context.Wrestlers
                .Include(w => w.Employee)
                .FirstOrDefault(
                    w => w.WrestlerId == wrestlerId);

        if (existingWrestler == null
            || existingWrestler.Employee == null)
        {
            return false;
        }

        existingWrestler.Employee.FirstName =
            employee.FirstName;

        existingWrestler.Employee.LastName =
            employee.LastName;

        existingWrestler.Employee.JobTitle =
            employee.JobTitle;

        existingWrestler.Employee.HireDate =
            employee.HireDate;

        existingWrestler.Employee.IsActive =
            employee.IsActive;

        existingWrestler.RingName =
            wrestler.RingName;

        existingWrestler.WeightClass =
            wrestler.WeightClass;

        existingWrestler.DebutDate =
            wrestler.DebutDate;

        existingWrestler.IsActive =
            wrestler.IsActive;

        _context.SaveChanges();

        return true;
    }

    public bool DeleteWithEmployee(int wrestlerId)
    {
        Wrestler? wrestler =
            _context.Wrestlers
                .Include(w => w.Employee)
                .FirstOrDefault(
                    w => w.WrestlerId == wrestlerId);

        if (wrestler == null
            || wrestler.Employee == null)
        {
            return false;
        }

        Employee employee =
            wrestler.Employee;

        _context.Wrestlers.Remove(wrestler);
        _context.Employees.Remove(employee);

        _context.SaveChanges();

        return true;
    }

    private IQueryable<WrestlerDetails> GetDetailsQuery()
    {
        return _context.Wrestlers
            .AsNoTracking()
            .Select(w => new WrestlerDetails
            {
                WrestlerId = w.WrestlerId,
                EmployeeId = w.EmployeeId,
                FirstName = w.Employee!.FirstName,
                LastName = w.Employee.LastName,
                JobTitle = w.Employee.JobTitle,
                HireDate = w.Employee.HireDate,
                RingName = w.RingName,
                WeightClass = w.WeightClass,
                DebutDate = w.DebutDate,
                IsActive = w.IsActive
            });
    }
}