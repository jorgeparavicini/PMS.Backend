using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<IEnumerable<AgencyDTO>>> GetAll()
    {
        return Ok(await _service.GetAllAgenciesAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AgencyDTO?>> Find(int id)
    {
        if (await _service.FindAgencyAsync(id) is { } agency)
        {
            return Ok(agency);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Models.Input.AgencyInputDTO agency)
    {
        if (await _service.CreateAgencyAsync(agency) is { } id)
        {
             return CreatedAtAction(nameof(Find), new { id }, null);
        }
        
        return BadRequest();
    }

}