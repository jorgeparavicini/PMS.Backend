using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Backend.Features.Frontend.Agency.Models;
using PMS.Backend.Features.Frontend.Agency.Models.Output;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Agency.Controllers;

[ApiController]
[Route("[controller]")]
public class AgencyController : ControllerBase
{
    private readonly IAgencyService _service;

    public AgencyController(IAgencyService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AgencyDTO>>> GetAllAsync()
    {
        return Ok(await _service.GetAllAgenciesAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AgencyDTO?>> FindAsync(int id)
    {
        if (await _service.FindAgencyAsync(id) is { } agency)
        {
            return Ok(agency);
        }

        return NotFound();
    }

}