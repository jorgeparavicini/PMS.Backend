using Microsoft.AspNetCore.Mvc;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Reservation.Models.Input;
using PMS.Backend.Features.Frontend.Reservation.Models.Output;
using PMS.Backend.Features.Frontend.Reservation.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Reservation.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationController(IReservationService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroupReservationDetailDTO>>> GetAll()
    {
        var result = await _service.GetAllGroupReservationsAsync();
        if (!result.Any())
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GroupReservationDetailDTO>> Find([FromRoute] int id)
    {
        if (await _service.FindGroupReservationAsync(id) is { } reservation)
        {
            return Ok(reservation);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<GroupReservationSummaryDTO>> Create(
        [FromBody] CreateGroupReservationDTO reservation)
    {
        try
        {
            var summary = await _service.CreateGroupReservationAsync(reservation);
            return CreatedAtAction(nameof(Find), new { summary.Id }, summary);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<GroupReservationSummaryDTO>> Update(
        [FromRoute] int id,
        [FromBody] UpdateGroupReservationDTO reservation)
    {
        if (id != reservation.Id)
        {
            return BadRequest("Group Reservation Id mismatch");
        }

        try
        {
            var summary = await _service.UpdateGroupReservationAsync(reservation);
            return Ok(summary);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _service.DeleteGroupReservationAsync(id);
        
        return NoContent();
    }
}