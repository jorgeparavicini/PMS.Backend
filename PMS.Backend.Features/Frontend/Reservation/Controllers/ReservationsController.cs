using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PMS.Backend.Common.Security;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.Common;
using PMS.Backend.Features.Frontend.Reservation.Models.Input;
using PMS.Backend.Features.Frontend.Reservation.Models.Input.Validators;

namespace PMS.Backend.Features.Frontend.Reservation.Controllers;

/// <summary>
/// A CRUD Controller for managing reservations.
/// </summary>
public class ReservationsController : ODataController
{
    private readonly Service<GroupReservation> _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReservationsController"/> class.
    /// </summary>
    /// <param name="service">
    /// An implementation of a reservation service to be used as a datastore.
    /// </param>
    public ReservationsController(Service<GroupReservation> service) => _service = service;

    /// <summary>
    /// A list of all group reservations.
    /// </summary>
    /// <returns>
    /// An action result with the HTTP status code and the reservations in the body.
    /// </returns>
    [EnableQuery]
    [HttpGet("reservations")]
    [Authorize(Policy = nameof(Policy.ReadReservations))]
    public IQueryable<GroupReservation> GetAll()
    {
        return _service.GetAll();
    }

    /// <summary>
    /// Searches for a group reservation with a given unique Id.
    /// </summary>
    /// <param name="id">The id of the reservation.</param>
    /// <returns>
    /// An action result with the HTTP status code and the full reservation in the body if it was
    /// found.
    /// </returns>
    [EnableQuery]
    [HttpGet("reservations({id:int})")]
    [Authorize(Policy = nameof(Policy.ReadReservations))]
    public SingleResult<GroupReservation> Find([FromRoute] int id)
    {
        return SingleResult.Create(_service.Find(id));
    }

    /// <summary>
    /// Creates a new group reservation.
    /// </summary>
    /// <param name="reservation">The content of the new reservation.</param>
    /// <returns>
    /// An action result with the HTTP status code, a header linking to the newly created resource
    /// and the new resource as the body.
    /// </returns>
    [EnableQuery]
    [HttpPost("reservations")]
    [Authorize(Policy = nameof(Policy.CreateReservations))]
    public async Task<IActionResult> Create(
        [FromBody] CreateGroupReservationDTO reservation)
    {
        var entity =
            await _service
                .CreateAsync<CreateGroupReservationDTO, CreateGroupReservationDTOValidator>(
                    reservation);
        return Created(entity);
    }

    /// <summary>
    /// Updates a group reservation.
    /// </summary>
    /// <param name="id">The id of the reservation to update.</param>
    /// <param name="reservation">The new content of the reservation.</param>
    /// <returns>
    /// An action result with the HTTP status code and the updated resource as the body.
    /// </returns>
    [EnableQuery]
    [HttpPut("reservations({id:int})")]
    [Authorize(Policy = nameof(Policy.UpdateReservations))]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UpdateGroupReservationDTO reservation)
    {
        var entity =
            await _service
                .UpdateAsync<UpdateGroupReservationDTO, UpdateGroupReservationDTOValidator>(id,
                    reservation);
        return Updated(entity);
    }

    /// <summary>
    /// Deletes a group reservation.
    /// </summary>
    /// <param name="id">The reservation to delete.</param>
    /// <returns>An action result with the HTTP status code and an empty body.</returns>
    [HttpDelete("reservations({id:int})")]
    [Authorize(Policy = nameof(Policy.DeleteReservations))]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}
