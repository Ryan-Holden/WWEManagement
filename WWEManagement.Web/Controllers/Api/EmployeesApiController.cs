using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Models;
using WWEManagement.Data.Repositories;

namespace WWEManagement.Web.Controllers.Api;

[ApiController]
[Route("api/employees")]
public class EmployeesApiController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeesApiController(
    IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public ActionResult<List<Employee>> GetAll()
    {
        List<Employee> employees =
            _employeeRepository.GetAll();

        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Employee> GetById(int id)
    {
        Employee? employee =
            _employeeRepository.GetById(id);

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost]
    public ActionResult<Employee> Create(
        Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            int newEmployeeId =
                _employeeRepository.Add(employee);

            employee.EmployeeId =
                newEmployeeId;

            return CreatedAtAction(
                nameof(GetById),
                new { id = newEmployeeId },
                employee);
        }
        catch (DbUpdateException)
        {
            return BadRequest(
                new
                {
                    message =
                        "The employee could not be created."
                });
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(
        int id,
        Employee employee)
    {
        if (id != employee.EmployeeId)
        {
            return BadRequest(
                new
                {
                    message =
                        "Route ID does not match EmployeeId."
                });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            bool updated =
                _employeeRepository.Update(employee);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (DbUpdateException)
        {
            return BadRequest(
                new
                {
                    message =
                        "The employee could not be updated."
                });
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            bool deleted =
                _employeeRepository.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (DbUpdateException)
        {
            return Conflict(
                new
                {
                    message =
                        "The employee could not be deleted because another record depends on it."
                });
        }
    }
}