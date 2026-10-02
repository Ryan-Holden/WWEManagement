using Microsoft.AspNetCore.Mvc;
using WWEManagement.Data.Models;
using WWEManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WWEManagement.Web.Controllers;

public class EmployeesController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeesController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult Index()
    {
        List<Employee> employees =
            _employeeRepository.GetAll();

        return View(employees);
    }

    public IActionResult Details(int id)
    {
        Employee? employee =
            _employeeRepository.GetById(id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return View(employee);
        }

        int newEmployeeId =
            _employeeRepository.Add(employee);

        return RedirectToAction(
            nameof(Details),
            new { id = newEmployeeId });
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Employee? employee =
            _employeeRepository.GetById(id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return View(employee);
        }

        bool updated =
            _employeeRepository.Update(employee);

        if (!updated)
        {
            return NotFound();
        }

        return RedirectToAction(
            nameof(Details),
            new { id = employee.EmployeeId });
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Employee? employee =
            _employeeRepository.GetById(id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            bool deleted =
                _employeeRepository.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            Employee? employee =
                _employeeRepository.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            ModelState.AddModelError(
                string.Empty,
                "This employee cannot be deleted because other records depend on them.");

            return View("Delete", employee);
        }
    }
}