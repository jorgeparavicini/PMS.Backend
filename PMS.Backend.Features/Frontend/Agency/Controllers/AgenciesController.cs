using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;
using PMS.Backend.Features.Models;

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

    /// <summary>
    /// Gets a summary of all agencies. The summary contains all properties of an agency except
    /// the list of contacts.
    /// </summary>
    /// <returns>An action result with the HTTP status code and the agencies in the body.</returns>
    [HttpGet]
    public async Task<ActionResult<PagedList<AgencySummaryDTO>>> GetAll()
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

        var summary = await _service.UpdateAgencyAsync(agency);
        return Ok(summary);
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
}
