using Microsoft.AspNetCore.Mvc;
using PMS.Backend.Core.Database;

namespace PMS.Backend.Features.Registration.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    public RegistrationController()
    {
        Console.WriteLine("Testing Code Coverage");
    }
}