using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Models;
using WWEManagement.Web.Dtos;
using WWEManagement.Web.Services;

namespace WWEManagement.Web.Controllers.Api;

[ApiController]
[Route("api/wrestlers")]
public class WrestlersApiController
    : ControllerBase
{
    private readonly IWrestlerService
        _wrestlerService;

    public WrestlersApiController(
        IWrestlerService wrestlerService)
    {
        _wrestlerService =
            wrestlerService;
    }

    [HttpGet]
    public ActionResult<List<WrestlerDetails>>
        GetAll()
    {
        List<WrestlerDetails> wrestlers =
            _wrestlerService.GetAllDetails();

        return Ok(wrestlers);
    }

    [HttpGet("{id:int}")]
    public ActionResult<WrestlerDetails>
        GetById(int id)
    {
        WrestlerDetails? wrestler =
            _wrestlerService.GetById(id);

        if (wrestler == null)
        {
            return NotFound();
        }

        return Ok(wrestler);
    }

    [HttpPost]
    public ActionResult<WrestlerDetails>
        Create(
            WrestlerRequestDto request)
    {
        try
        {
            int wrestlerId =
                _wrestlerService.Create(
                    request);

            WrestlerDetails? created =
                _wrestlerService.GetById(
                    wrestlerId);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    id = wrestlerId
                },
                created);
        }
        catch (DbUpdateException)
        {
            return BadRequest(
                new
                {
                    message =
                        "The wrestler could not be created."
                });
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(
        int id,
        WrestlerRequestDto request)
    {
        try
        {
            bool updated =
                _wrestlerService.Update(
                    id,
                    request);

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
                        "The wrestler could not be updated."
                });
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            bool deleted =
                _wrestlerService.Delete(id);

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
                        "The wrestler could not be deleted because another record depends on it."
                });
        }
    }
}