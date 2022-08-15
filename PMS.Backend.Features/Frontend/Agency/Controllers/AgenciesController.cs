using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PMS.Backend.Common.Security;
using PMS.Backend.Features.Attributes;
using PMS.Backend.Features.Common;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;

namespace PMS.Backend.Features.Frontend.Agency.Controllers;

/// <summary>
/// A CRUD Controller for managing agencies and its contacts.
/// </summary>
[ApiController]
public class AgenciesController : ODataController
{
    private readonly Service<Core.Entities.Agency.Agency> _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="AgenciesController" /> class.
    /// </summary>
    /// <param name="service">
    /// An implementation of an agency service to be used as a datastore.
    /// </param>
    public AgenciesController(Service<Core.Entities.Agency.Agency> service) => _service = service;

    /// <summary>
    /// A list of all agencies.
    /// </summary>
    /// <returns>An action result with the HTTP status code and the agencies in the body.</returns>
    /// <response code="200">If the operation completed successfully.</response>
    /// <response code="default">If an unknown error occurred.</response>
    [EnableQuery]
    [HttpGet("agencies")]
    [Authorize(Policy = nameof(Policy.ReadAgencies))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public IQueryable<Core.Entities.Agency.Agency> GetAll()
    {
        return _service.GetAll();
    }

    /// <summary>
    /// Searches for an agency with a given unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the agency.</param>
    /// <returns>
    /// An action result with the HTTP status code and the full agency in the body if it was found.
    /// </returns>
    /// <response code="200">If the operation completed successfully.</response>
    /// <response code="404">If an entity was not found.</response>
    /// <response code="default">If an unknown error occurred.</response>
    [EnableQuery]
    [HttpGet("agencies({id:int})")]
    [Authorize(Policy = nameof(Policy.ReadAgencies))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public SingleResult<Core.Entities.Agency.Agency> Find([FromRoute] int id)
    {
        return SingleResult.Create(_service.Find(id));
    }

    /// <summary>
    /// Creates a new agency.
    /// </summary>
    /// <param name="agency">The content of the new agency.</param>
    /// <returns>
    /// An action result with the HTTP status code, a header linking to the newly created resource
    /// and the new resource as the body.
    /// </returns>
    /// <response code="201">If the agency was successfully created.</response>
    /// <response code="204">
    /// If the agency was successfully created and <code>Prefer</code> header
    /// is set to <code>return=minimal</code>
    /// </response>
    /// <response code="400">If the input data contained validation errors.</response>
    /// <response code="default">If an unknown error occurred.</response>
    [EnableQuery]
    [HttpPost("agencies")]
    [Authorize(Policy = nameof(Policy.CreateAgencies))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create(
        [FromBody] CreateAgencyDTO agency)
    {
        var entity = await _service.CreateAsync<CreateAgencyDTO, CreateAgencyDTOValidator>(agency);
        return Created(entity);
    }

    /// <summary>
    /// Updates a given agency.
    /// </summary>
    /// <param name="id">The id of the agency which should be updated.</param>
    /// <param name="agency">The new content of the agency.</param>
    /// <returns>
    /// An action result with the HTTP status code and the updated resource as the body.
    /// </returns>
    /// <response code="200">
    /// If the agency was updated successfully and the <code>Prefer</code> header is set to
    /// <code>return=representation</code>.
    /// </response>
    /// <response code="204">If the agency was updated successfully.</response>
    /// <response code="400">If the input data contained validation errors.</response>
    /// <response code="404">If an entity was not found.</response>
    /// <response code="default">If an unknown error occurred.</response>
    [EnableQuery]
    [DisableSwaggerQuery]
    [HttpPut("agencies({id:int})")]
    [Authorize(Policy = nameof(Policy.UpdateAgencies))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UpdateAgencyDTO agency)
    {
        var entity =
            await _service.UpdateAsync<UpdateAgencyDTO, UpdateAgencyDTOValidator>(id, agency);
        return Updated(entity);
    }

    /// <summary>
    /// Deletes an agency.
    /// </summary>
    /// <param name="id">The agency to be deleted.</param>
    /// <returns>
    /// An action result with the HTTP status code, and empty body.
    /// </returns>
    /// <response code="204">If the agency was successfully deleted.</response>
    /// <response code="default">If an unknown error occurred.</response>
    [HttpDelete("agencies({id:int})")]
    [Authorize(Policy = nameof(Policy.DeleteAgencies))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}
