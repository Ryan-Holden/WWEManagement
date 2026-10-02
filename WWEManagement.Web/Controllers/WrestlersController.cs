using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Models;
using WWEManagement.Web.Services;
using WWEManagement.Web.ViewModels;

namespace WWEManagement.Web.Controllers;

public class WrestlersController : Controller
{
    private readonly IWrestlerService _wrestlerService;

    public WrestlersController(
        IWrestlerService wrestlerService)
    {
        _wrestlerService = wrestlerService;
    }

    public IActionResult Index()
    {
        List<Wrestler> wrestlers =
            _wrestlerService.GetAll();

        return View(wrestlers);
    }

    public IActionResult Details(int id)
    {
        WrestlerDetails? wrestler =
            _wrestlerService.GetById(id);

        if (wrestler == null)
        {
            return NotFound();
        }

        return View(wrestler);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(
            new CreateWrestlerViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(
        CreateWrestlerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            int wrestlerId =
                _wrestlerService.Create(model);

            return RedirectToAction(
                nameof(Details),
                new { id = wrestlerId });
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(
                string.Empty,
                "The wrestler could not be created. Please try again.");

            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        WrestlerDetails? wrestler =
            _wrestlerService.GetById(id);

        if (wrestler == null)
        {
            return NotFound();
        }

        EditWrestlerViewModel model =
            new EditWrestlerViewModel
            {
                WrestlerId =
                    wrestler.WrestlerId,

                EmployeeId =
                    wrestler.EmployeeId,

                FirstName =
                    wrestler.FirstName,

                LastName =
                    wrestler.LastName,

                HireDate =
                    wrestler.HireDate,

                RingName =
                    wrestler.RingName,

                WeightClass =
                    wrestler.WeightClass,

                DebutDate =
                    wrestler.DebutDate,

                IsActive =
                    wrestler.IsActive
            };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(
        EditWrestlerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            bool updated =
                _wrestlerService.Update(model);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(
                nameof(Details),
                new { id = model.WrestlerId });
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(
                string.Empty,
                "The wrestler could not be updated. Please try again.");

            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        WrestlerDetails? wrestler =
            _wrestlerService.GetById(id);

        if (wrestler == null)
        {
            return NotFound();
        }

        return View(wrestler);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(
        int id)
    {
        WrestlerDetails? wrestler =
            _wrestlerService.GetById(id);

        if (wrestler == null)
        {
            return NotFound();
        }

        try
        {
            bool deleted =
                _wrestlerService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(
                nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(
                string.Empty,
                "The wrestler could not be deleted because other records may depend on this employee.");

            return View(
                "Delete",
                wrestler);
        }
    }
}