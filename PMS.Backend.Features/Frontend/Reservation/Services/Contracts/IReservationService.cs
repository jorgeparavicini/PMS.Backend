using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Reservation.Models.Input;
using PMS.Backend.Features.Frontend.Reservation.Models.Output;

namespace PMS.Backend.Features.Frontend.Reservation.Services.Contracts;

/// <summary>
/// A service responsible for managing all reservation queries and mutations.
/// </summary>
/// <remarks>
/// No matter what kind of methods are called with no matter the arguments, the state of any
/// reservation will never be invalid.
/// </remarks>
public interface IReservationService
{
    /// <summary>
    /// Gets all reservations from the database context.
    /// This will return a reduced image of the group reservations without the individual
    /// reservations. 
    /// </summary>
    /// <returns>An async enumerable containing all group reservations.</returns>
    Task<IEnumerable<GroupReservationSummaryDTO>> GetAllGroupReservationsAsync();

    /// <summary>
    /// Tries to find a reservation with the given id.
    /// This will return the full group reservation including all reservations and their details.
    /// </summary>
    /// <param name="id">The id of the group reservation.</param>
    /// <returns>The group reservation or null if it wasn't found.</returns>
    Task<GroupReservationDetailDTO?> FindGroupReservationAsync(int id);

    /// <summary>
    /// Creates a new group reservation in the db context.
    /// </summary>
    /// <param name="reservation">The reservation to be added to the database.</param>
    /// <returns>
    /// An image of the group reservation excluding the reservations and their details.
    /// </returns>
    /// <exception cref="BadRequestException">
    /// Thrown if the passed reservation is invalid.
    /// </exception>
    /// <remarks>The changes are automatically saved.</remarks>
    Task<GroupReservationSummaryDTO> CreateGroupReservationAsync(
        CreateGroupReservationDTO reservation);

    /// <summary>
    /// Updates an existing reservation and saves the changes to the db.
    /// If the reservation does not exist it will not create it, an error is thrown instead.
    /// </summary>
    /// <param name="reservation">
    /// The new reservation.
    /// The id of this object will be used to look for the database entity.
    /// </param>
    /// <returns>
    /// An image of the updated group reservation excluding the reservation and their details.
    /// </returns>
    /// <exception cref="BadRequestException">
    /// Thrown if the new group reservation object contained validation errors.
    /// </exception>
    /// <exception cref="NotFoundException">
    /// Thrown if the group reservation was not found in the db.
    /// </exception>
    /// <remarks>The changes are automatically saved.</remarks>
    Task<GroupReservationSummaryDTO> UpdateGroupReservationAsync(
        UpdateGroupReservationDTO reservation);

    /// <summary>
    /// Deletes a group reservation and all associated reservations and details.
    /// </summary>
    /// <param name="id">The id of the group reservation to be deleted.</param>
    /// <remarks>
    /// The reservation can only be deleted if no other entity references any of the entities
    /// in this group reservation.
    /// The changes are automatically saved.
    /// </remarks>
    Task DeleteGroupReservationAsync(int id);
}
