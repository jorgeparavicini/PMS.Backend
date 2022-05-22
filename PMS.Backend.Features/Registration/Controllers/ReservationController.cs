using Microsoft.AspNetCore.Mvc;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Registration.Services.Contracts;

namespace PMS.Backend.Features.Registration.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _service;
    
    public ReservationController(IReservationService reservationService)
    {
        _service = reservationService;
    }
}