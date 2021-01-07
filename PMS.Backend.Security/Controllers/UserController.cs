using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMS.Backend.Security.Entity;

namespace PMS.Backend.Security.Controllers
{
    [ApiController]
    [Authorize]
    [Route("auth/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Models.User), StatusCodes.Status200OK)]
        public async Task<ActionResult<Models.User>> GetCurrentUser()
        {
            if (!(HttpContext.User is { } principal))
            {
                return Unauthorized();
            }

            var user = await _userManager.GetUserAsync(principal);
            return Ok(_mapper.Map<Models.User>(user));
        }
    }
}
