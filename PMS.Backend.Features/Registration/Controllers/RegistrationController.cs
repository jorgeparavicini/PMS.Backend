using Microsoft.AspNetCore.Mvc;
using PMS.Backend.Core.Database;

namespace PMS.Backend.Features.Registration.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly PmsDbContext _dbContext;
    
    public RegistrationController(PmsDbContext dbContext) => _dbContext = dbContext;
}