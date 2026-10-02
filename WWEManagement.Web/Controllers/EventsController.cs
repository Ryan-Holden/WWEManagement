using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Models;
using WWEManagement.Data.Repositories;

namespace WWEManagement.Web.Controllers;

public class EventsController : Controller
{
    private readonly IEventRepository _eventRepository;

    public EventsController(
    IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public IActionResult Index()
    {
        List<Event> events =
            _eventRepository.GetAll();

        return View(events);
    }

    public IActionResult Upcoming()
    {
        List<Event> events =
            _eventRepository.GetUpcoming(
                DateTime.Now);

        return View(events);
    }

    public IActionResult Details(int id)
    {
        Event? wrestlingEvent =
            _eventRepository.GetById(id);

        if (wrestlingEvent == null)
        {
            return NotFound();
        }

        return View(wrestlingEvent);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Event
        {
            EventDate = DateTime.Today,
            Status = "Scheduled"
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(
        Event wrestlingEvent)
    {
        if (!ModelState.IsValid)
        {
            return View(wrestlingEvent);
        }

        try
        {
            int eventId =
                _eventRepository.Add(
                    wrestlingEvent);

            return RedirectToAction(
                nameof(Details),
                new { id = eventId });
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(
                string.Empty,
                "The event could not be created.");

            return View(wrestlingEvent);
        }
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Event? wrestlingEvent =
            _eventRepository.GetById(id);

        if (wrestlingEvent == null)
        {
            return NotFound();
        }

        return View(wrestlingEvent);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(
        Event wrestlingEvent)
    {
        if (!ModelState.IsValid)
        {
            return View(wrestlingEvent);
        }

        try
        {
            bool updated =
                _eventRepository.Update(
                    wrestlingEvent);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id = wrestlingEvent.EventId
                });
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(
                string.Empty,
                "The event could not be updated.");

            return View(wrestlingEvent);
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Event? wrestlingEvent =
            _eventRepository.GetById(id);

        if (wrestlingEvent == null)
        {
            return NotFound();
        }

        return View(wrestlingEvent);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            bool deleted =
                _eventRepository.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(
                nameof(Index));
        }
        catch (DbUpdateException)
        {
            Event? wrestlingEvent =
                _eventRepository.GetById(id);

            if (wrestlingEvent == null)
            {
                return NotFound();
            }

            ModelState.AddModelError(
                string.Empty,
                "The event could not be deleted because another record may depend on it.");

            return View(
                "Delete",
                wrestlingEvent);
        }
    }
}