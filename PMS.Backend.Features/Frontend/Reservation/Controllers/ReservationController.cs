using Microsoft.AspNetCore.Mvc;
using PMS.Backend.Features.Frontend.Reservation.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Reservation.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _service;
    
    public ReservationController(IReservationService reservationService)
    {
        _service = reservationService;
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent()
    {
        return Ok();
    }
    
    [HttpGet("error")]
    public async Task<IActionResult> GetSomething()
    {
        return NotFound();
    }
    
    [HttpPost("error")]
    public async Task<IActionResult> GetError()
    {
        return NotFound();
    }
}