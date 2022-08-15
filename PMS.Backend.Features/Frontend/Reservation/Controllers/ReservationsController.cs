using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PMS.Backend.Common.Security;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.Attributes;
using PMS.Backend.Features.Common;
using PMS.Backend.Features.Frontend.Reservation.Models.Input;
using PMS.Backend.Features.Frontend.Reservation.Models.Input.Validators;

namespace PMS.Backend.Features.Frontend.Reservation.Controllers;

/// <summary>
/// A CRUD Controller for managing reservations.
/// </summary>
[ApiController]
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
    /// <response code="200">If the operation completed successfully.</response>
    /// <response code="default">If an unknown error occurred.</response>
    [EnableQuery]
    [HttpGet("reservations")]
    [Authorize(Policy = nameof(Policy.ReadReservations))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
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
    /// <response code="200">If the operation completed successfully.</response>
    /// <response code="404">If an entity was not found.</response>
    /// <response code="default">If an unknown error occurred.</response>
    [EnableQuery]
    [HttpGet("reservations({id:int})")]
    [Authorize(Policy = nameof(Policy.ReadReservations))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
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
    /// <response code="201">If the reservation was successfully created.</response>
    /// <response code="204">
    /// If the reservation was successfully created and <code>Prefer</code> header
    /// is set to <code>return=minimal</code>
    /// </response>
    /// <response code="400">If the input data contained validation errors.</response>
    /// <response code="default">If an unknown error occurred.</response>
    [EnableQuery]
    [HttpPost("reservations")]
    [Authorize(Policy = nameof(Policy.CreateReservations))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
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
    /// <response code="200">
    /// If the reservation was updated successfully and the <code>Prefer</code> header is set to
    /// <code>return=representation</code>.
    /// </response>
    /// <response code="204">If the reservation was updated successfully.</response>
    /// <response code="400">If the input data contained validation errors.</response>
    /// <response code="404">If an entity was not found.</response>
    /// <response code="default">If an unknown error occurred.</response>
    [EnableQuery]
    [DisableSwaggerQuery]
    [HttpPut("reservations({id:int})")]
    [Authorize(Policy = nameof(Policy.UpdateReservations))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
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
    /// <response code="204">If the group reservation was successfully deleted.</response>
    /// <response code="default">If an unknown error occurred.</response>
    [HttpDelete("reservations({id:int})")]
    [Authorize(Policy = nameof(Policy.DeleteReservations))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}
