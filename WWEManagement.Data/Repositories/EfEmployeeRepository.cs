using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Data;
using WWEManagement.Data.Models;

namespace WWEManagement.Data.Repositories;

public class EfEmployeeRepository : IEmployeeRepository
{
    private readonly WWEManagementDbContext _context;

    public EfEmployeeRepository(
        WWEManagementDbContext context)
    {
        _context = context;
    }

    public List<Employee> GetAll()
    {
        return _context.Employees
            .AsNoTracking()
            .OrderBy(e => e.EmployeeId)
            .ToList();
    }

    public Employee? GetById(int id)
    {
        return _context.Employees
            .AsNoTracking()
            .FirstOrDefault(e => e.EmployeeId == id);
    }

    public int Add(Employee employee)
    {
        _context.Employees.Add(employee);

        _context.SaveChanges();

        return employee.EmployeeId;
    }

    public bool Update(Employee employee)
    {
        Employee? existing =
            _context.Employees
                .FirstOrDefault(
                    e => e.EmployeeId == employee.EmployeeId);

        if (existing == null)
        {
            return false;
        }

        existing.FirstName =
            employee.FirstName;

        existing.LastName =
            employee.LastName;

        existing.JobTitle =
            employee.JobTitle;

        existing.HireDate =
            employee.HireDate;

        existing.IsActive =
            employee.IsActive;

        _context.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        Employee? employee =
            _context.Employees
                .FirstOrDefault(e => e.EmployeeId == id);

        if (employee == null)
        {
            return false;
        }

        _context.Employees.Remove(employee);

        _context.SaveChanges();

        return true;
    }
}