using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;

namespace PMS.Backend.Features.Frontend.Agency.Services.Contracts;

/// <summary>
/// A service responsible for managing all queries and mutations regarding agencies.
/// </summary>
/// <remarks>
/// No matter what kind of methods are called with no matter the arguments, the state of any agency
/// will never be invalid.
/// </remarks>
public interface IAgencyService
{
    #region Agency

    /// <summary>
    /// Gets all agencies from the database context.
    /// This will return a reduced image of the agencies without the contacts to preserve bandwidth.
    /// </summary>
    /// <returns>An async enumerable containing all agencies.</returns>
    Task<IEnumerable<AgencySummaryDTO>> GetAllAgenciesAsync();

    /// <summary>
    /// Tries to find an agency with the given unique id.
    /// This will return the full agency containing all contacts.
    /// </summary>
    /// <param name="id">The id of the agency to be found.</param>
    /// <returns>The agency or null if no agency with the id exists.</returns>
    Task<AgencyDetailDTO?> FindAgencyAsync(int id);

    /// <summary>
    /// Creates a new Agency in the PMS database context.
    /// </summary>
    /// <param name="agency">The agency to be created.</param>
    /// <returns>An image of the new agency excluding the contacts.</returns>
    /// <remarks>The changes are automatically saved.</remarks>
    Task<AgencySummaryDTO> CreateAgencyAsync(CreateAgencyDTO agency);


    /// <summary>
    /// Updates an existing agency and saves the changes to the PMS database.
    /// If the agency does not exist it will not create it, an error is thrown instead.
    /// </summary>
    /// <param name="agency">
    /// The agency to be updated.
    /// The id of this object will be used to look for the database entity.
    /// </param>
    /// <returns>An image of the updated agency excluding the contacts.</returns>
    /// <exception cref="NotFoundException">
    /// Thrown if the agency was not found in the db.
    /// </exception>
    /// <remarks>The changes are automatically saved.</remarks>
    Task<AgencySummaryDTO> UpdateAgencyAsync(UpdateAgencyDTO agency);

    /// <summary>
    /// Deletes an agency from the database if and only if no other entities reference its contacts.
    /// </summary>
    /// <param name="id">The id of the agency to be deleted.</param>
    /// <remarks>The changes are automatically saved.</remarks>
    Task DeleteAgencyAsync(int id);

    #endregion

    #region Agency Contact

    /// <summary>
    /// Gets all contacts for an agency.
    /// </summary>
    /// <param name="agencyId">The id of the agency.</param>
    /// <returns>A list of all contacts for the agency.</returns>
    /// <exception cref="NotFoundException">Thrown if the agency was not found.</exception>
    Task<IEnumerable<AgencyContactDTO>> GetAllContactsForAgencyAsync(int agencyId);

    /// <summary>
    /// Tries to find a contact for an agency.
    /// </summary>
    /// <param name="agencyId">
    /// The agency of the contact.
    /// TODO: This param is redundant and should be removed.
    /// </param>
    /// <param name="contactId">
    /// The id of the contact.
    /// </param>
    /// <returns>The agency contact or null if it was not found.</returns>
    Task<AgencyContactDTO?> FindContactForAgency(int agencyId, int contactId);

    /// <summary>
    /// Creates a new contact for an existing agency.
    /// </summary>
    /// <param name="agencyId">The id of the agency.</param>
    /// <param name="contact">The content of the new contact.</param>
    /// <returns>The newly created contact with the db assigned id.</returns>
    /// <exception cref="NotFoundException">Thrown if the agency was not found.</exception>
    /// <remarks>The changes are automatically saved.</remarks>
    Task<AgencyContactDTO> CreateContactForAgencyAsync(
        int agencyId,
        CreateAgencyContactDTO contact);

    /// <summary>
    /// Updates an existing contact for a agency.
    /// </summary>
    /// <param name="agencyId">
    /// The id of the agency the contact belongs to.
    /// TODO: This parameter is redundant and should be removed.
    /// </param>
    /// <param name="contact">The new contact object.</param>
    /// <returns>The updated agency </returns>
    /// <exception cref="NotFoundException"></exception>
    /// <remarks>The changes are automatically saved.</remarks>
    Task<AgencyContactDTO> UpdateContactForAgencyAsync(
        int agencyId,
        UpdateAgencyContactDTO contact);

    /// <summary>
    /// Deletes an agency contact.
    /// This is only allowed if the contact is not referenced by any other entities.
    /// </summary>
    /// <param name="agencyId">
    /// The id of the agency of the contact.
    /// TODO: Redundant
    /// </param>
    /// <param name="contactId">The id of the contact.</param>
    /// <remarks>The changes are automatically saved.</remarks>
    Task DeleteAgencyContactAsync(int agencyId, int contactId);

    #endregion


}
