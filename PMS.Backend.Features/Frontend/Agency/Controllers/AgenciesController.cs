using Microsoft.AspNetCore.Mvc;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Agency.Controllers;

[ApiController]
[Route("{controller}")]
public class AgencyController : ControllerBase
{
    private readonly IAgencyService _service;

    public AgencyController(IAgencyService service) => _service = service;

    #region Agency

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

    [HttpPost]
    public async Task<ActionResult<AgencySummaryDTO>> Create(
        [FromBody] CreateAgencyDTO agency)
    {
        try
        {
            var summary = await _service.CreateAgencyAsync(agency);
            return CreatedAtAction(nameof(Find), new { summary.Id }, summary);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }

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
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        await _service.DeleteAgencyAsync(id);

        return NoContent();
    }

    #endregion

    #region Agency Contact

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
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }

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
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }

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