using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Models;
using WWEManagement.Data.Repositories;
using WWEManagement.Web.Dtos;

namespace WWEManagement.Web.Controllers.Api;

[ApiController]
[Route("api/events")]
public class EventsApiController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    public EventsApiController(
    IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    [HttpGet]
    public ActionResult<List<Event>> GetAll()
    {
        return Ok(
            _eventRepository.GetAll());
    }

    [HttpGet("upcoming")]
    public ActionResult<List<Event>> GetUpcoming()
    {
        return Ok(
            _eventRepository.GetUpcoming(
                DateTime.Now));
    }

    [HttpGet("{id:int}")]
    public ActionResult<Event> GetById(int id)
    {
        Event? wrestlingEvent =
            _eventRepository.GetById(id);

        if (wrestlingEvent == null)
        {
            return NotFound();
        }

        return Ok(wrestlingEvent);
    }

    [HttpPost]
    public ActionResult<Event> Create(
        EventRequestDto request)
    {
        Event wrestlingEvent =
            MapRequest(request);

        try
        {
            int eventId =
                _eventRepository.Add(
                    wrestlingEvent);

            wrestlingEvent.EventId =
                eventId;

            return CreatedAtAction(
                nameof(GetById),
                new { id = eventId },
                wrestlingEvent);
        }
        catch (DbUpdateException)
        {
            return BadRequest(new
            {
                message =
                    "The event could not be created."
            });
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(
        int id,
        EventRequestDto request)
    {
        Event? existing =
            _eventRepository.GetById(id);

        if (existing == null)
        {
            return NotFound();
        }

        Event wrestlingEvent =
            MapRequest(request);

        wrestlingEvent.EventId = id;

        try
        {
            bool updated =
                _eventRepository.Update(
                    wrestlingEvent);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new
            {
                message =
                    "The event could not be updated."
            });
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            bool deleted =
                _eventRepository.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (DbUpdateException)
        {
            return Conflict(new
            {
                message =
                    "The event could not be deleted because another record depends on it."
            });
        }
    }

    private static Event MapRequest(
        EventRequestDto request)
    {
        return new Event
        {
            EventName = request.EventName,
            EventDate = request.EventDate,
            Venue = request.Venue,
            City = request.City,
            State = request.State,
            Status = request.Status
        };
    }
}