using Microsoft.AspNetCore.Mvc;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Agency.Controllers;

/// <summary>
/// A CRUD Controller for managing agencies and its contacts.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AgenciesController : ControllerBase
{
    private readonly IAgencyService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="AgenciesController" /> class.
    /// </summary>
    /// <param name="service">
    /// An implementation of an agency service to be used as a datastore.
    /// </param>
    public AgenciesController(IAgencyService service) => _service = service;

    #region Agency

    /// <summary>
    /// Gets a summary of all agencies. The summary contains all properties of an agency except
    /// the list of contacts.
    /// </summary>
    /// <returns>An action result with the HTTP status code and the agencies in the body.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgencySummaryDTO>>> GetAll()
    {
        var result = await _service.GetAllAgenciesAsync();
        if (!result.Any())
        {
            return NoContent();
        }

        return Ok(result);
    }

    /// <summary>
    /// Searches for an agency with a given unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the agency.</param>
    /// <returns>
    /// An action result with the HTTP status code and the full agency in the body if it was found.
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AgencyDetailDTO?>> Find(
        [FromRoute] int id)
    {
        if (await _service.FindAgencyAsync(id) is { } agency)
        {
            return Ok(agency);
        }

        return NotFound();
    }

    /// <summary>
    /// Creates a new agency.
    /// </summary>
    /// <param name="agency">The content of the new agency.</param>
    /// <returns>
    /// An action result with the HTTP status code, a header linking to the newly created resource
    /// and the new resource as the body.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<AgencySummaryDTO>> Create(
        [FromBody] CreateAgencyDTO agency)
    {
        var summary = await _service.CreateAgencyAsync(agency);
        return CreatedAtAction(nameof(Find), new { summary.Id }, summary);
    }

    /// <summary>
    /// Updates a given agency. To update its contacts use appropriate agency contacts CRUD methods.
    /// </summary>
    /// <param name="id">The id of the agency which should be updated.</param>
    /// <param name="agency">The new content of the agency.</param>
    /// <returns>
    /// An action result with the HTTP status code and the updated resource as the body.
    /// </returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<AgencySummaryDTO>> Update(
        [FromRoute] int id,
        [FromBody] UpdateAgencyDTO agency)
    {
        if (id != agency.Id)
        {
            return BadRequest("Agency Id mismatch");
        }

        try
        {
            var summary = await _service.UpdateAgencyAsync(agency);
            return Ok(summary);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Deletes an agency. Agencies will not be deleted if any other components rely on a contact
    /// of the agency.
    /// </summary>
    /// <param name="id">The agency to be deleted.</param>
    /// <returns>
    /// An action result with the HTTP status code, and empty body.
    /// </returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        await _service.DeleteAgencyAsync(id);

        return NoContent();
    }

    #endregion

    #region Agency Contact

    /// <summary>
    /// Gets a list of all contacts for a given agency.
    /// </summary>
    /// <param name="agencyId">The unique identifier of the agency.</param>
    /// <returns>
    /// An action result with the HTTP status code and the list of found contacts as the body.
    /// </returns>
    [HttpGet("{agencyId:int}/contacts")]
    public async Task<ActionResult<IEnumerable<AgencyContactDTO>>> FindAllContactsForAgency(
        [FromRoute] int agencyId)
    {
        try
        {
            var result = await _service.GetAllContactsForAgencyAsync(agencyId);
            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Finds a contact of a given agency.
    /// </summary>
    /// <param name="agencyId">The id of the agency.</param>
    /// <param name="contactId">The id of the contact.</param>
    /// <returns>
    /// An action result with the HTTP status code and the contact as the body.
    /// </returns>
    /// <remarks>
    /// This method should not rely on the agency id as the contact ids are globally unique.
    /// </remarks>
    [HttpGet("{agencyId:int}/contacts/{contactId:int}")]
    public async Task<ActionResult<AgencyContactDTO>> FindContact(
        [FromRoute] int agencyId,
        [FromRoute] int contactId)
    {
        if (await _service.FindContactForAgency(agencyId, contactId) is { } contact)
        {
            return Ok(contact);
        }

        return NotFound();
    }

    /// <summary>
    /// Adds a new contact to the contact list of an agency.
    /// </summary>
    /// <param name="agencyId">The unique identifier of the agency.</param>
    /// <param name="contact">The content of the new contact.</param>
    /// <returns>
    /// An action result with the HTTP status code, a header linking to the newly created resource
    /// and the new resource as the body.
    /// </returns>
    [HttpPost("{agencyId:int}/contacts")]
    public async Task<ActionResult<AgencyContactDTO>> CreateContact(
        [FromRoute] int agencyId,
        [FromBody] CreateAgencyContactDTO contact)
    {
        try
        {
            var newContact = await _service.CreateContactForAgencyAsync(agencyId, contact);
            return CreatedAtAction(nameof(FindContact),
                new { agencyId, newContact.Id },
                newContact);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Updates a contact for an agency.
    /// </summary>
    /// <param name="agencyId">The id of the agency.</param>
    /// <param name="contactId">The id of the contact.</param>
    /// <param name="contact">The new content of the contact.</param>
    /// <returns>
    /// An action result with the HTTP status code and the new resource as the body.
    /// </returns>
    [HttpPut("{agencyId:int}/contacts/{contactId:int}")]
    public async Task<ActionResult<AgencyContactDTO>> UpdateContact(
        [FromRoute] int agencyId,
        [FromRoute] int contactId,
        [FromBody] UpdateAgencyContactDTO contact)
    {
        if (contactId != contact.Id)
        {
            return BadRequest("Contact Id mismatch");
        }

        try
        {
            var updatedContact = await _service.UpdateContactForAgencyAsync(agencyId, contact);
            return Ok(updatedContact);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Deletes a contact for an agency if it is not referenced anywhere.
    /// </summary>
    /// <param name="agencyId">The id of the agency.</param>
    /// <param name="contactId">The id of the contact.</param>
    /// <returns>The HTTP status code.</returns>
    /// <remarks>
    /// If the contact is referenced in any other entity, the method will not delete the contact.
    /// TODO: This check has to be created yet
    /// </remarks>
    [HttpDelete("{agencyId:int}/contacts/{contactId:int}")]
    public async Task<IActionResult> DeleteAgencyContact(
        [FromRoute] int agencyId,
        [FromRoute] int contactId)
    {
        await _service.DeleteAgencyContactAsync(agencyId, contactId);

        return NoContent();
    }

    #endregion
}
