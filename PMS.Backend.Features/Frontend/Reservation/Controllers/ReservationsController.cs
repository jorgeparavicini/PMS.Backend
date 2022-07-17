using Microsoft.AspNetCore.Mvc;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Reservation.Models.Input;
using PMS.Backend.Features.Frontend.Reservation.Models.Output;
using PMS.Backend.Features.Frontend.Reservation.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Reservation.Controllers;

/// <summary>
/// A CRUD Controller for managing reservations.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReservationsController"/> class.
    /// </summary>
    /// <param name="service">
    /// An implementation of a reservation service to be used as a datastore.
    /// </param>
    public ReservationsController(IReservationService service) => _service = service;

    /// <summary>
    /// Gets a summary of all reservations. The summary contains only the topmost properties.
    /// </summary>
    /// <returns>
    /// An action result with the HTTP status code and the reservations in the body.
    /// </returns>
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

    /// <summary>
    /// Searches for a reservation with a given unique Id.
    /// </summary>
    /// <param name="id">The id of the reservation.</param>
    /// <returns>
    /// An action result with the HTTP status code and the full reservation in the body if it was
    /// found.
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GroupReservationDetailDTO>> Find([FromRoute] int id)
    {
        if (await _service.FindGroupReservationAsync(id) is { } reservation)
        {
            return Ok(reservation);
        }

        return NotFound();
    }

    /// <summary>
    /// Creates a new reservation.
    /// </summary>
    /// <param name="reservation">The content of the new reservation.</param>
    /// <returns>
    /// An action result with the HTTP status code, a header linking to the newly created resource
    /// and the new resource as the body.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<GroupReservationSummaryDTO>> Create(
        [FromBody] CreateGroupReservationDTO reservation)
    {
        var summary = await _service.CreateGroupReservationAsync(reservation);
        return CreatedAtAction(nameof(Find), new { summary.Id }, summary);
    }

    /// <summary>
    /// Updates a reservation and child entities.
    /// </summary>
    /// <param name="id">The id of the reservation to update.</param>
    /// <param name="reservation">The new content of the reservation.</param>
    /// <returns>
    /// An action result with the HTTP status code and the updated resource as the body.
    /// </returns>
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
    }

    /// <summary>
    /// Deletes a reservation if and only if there are no other entities relying on it.
    /// </summary>
    /// <param name="id">The reservation to delete.</param>
    /// <returns>An action result with the HTTP status code and an empty body.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _service.DeleteGroupReservationAsync(id);

        return NoContent();
    }
}
