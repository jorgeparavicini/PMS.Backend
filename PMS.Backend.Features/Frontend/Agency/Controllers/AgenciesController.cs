using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PMS.Backend.Common.Security;
using PMS.Backend.Features.Common;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;

namespace PMS.Backend.Features.Frontend.Agency.Controllers;

/// <summary>
/// A CRUD Controller for managing agencies and its contacts.
/// </summary>
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
    [EnableQuery]
    [HttpGet("agencies")]
    [Authorize(Policy = nameof(Policy.ReadAgencies))]
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
    [EnableQuery]
    [HttpGet("agencies({id:int})")]
    [Authorize(Policy = nameof(Policy.ReadAgencies))]
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
    [EnableQuery]
    [HttpPost("agencies")]
    [Authorize(Policy = nameof(Policy.CreateAgencies))]
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
    [EnableQuery]
    [HttpPut("agencies({id:int})")]
    [Authorize(Policy = nameof(Policy.UpdateAgencies))]
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
    [HttpDelete("agencies({id:int})")]
    [Authorize(Policy = nameof(Policy.DeleteAgencies))]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}
