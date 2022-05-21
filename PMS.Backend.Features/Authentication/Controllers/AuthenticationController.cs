namespace PMS.Backend.Features.Authentication.Controllers;

/*[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthenticationController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet("user")]
    public ActionResult<string> GetLoggedInUser()
    {
        return Ok(HttpContext.User.Identity?.Name);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> Login(LoginModel input)
    { 
        var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, input
        .RememberMe, true);
        if (result.Succeeded)
        {
            return Ok("Successfully logged in");
        }

        return Unauthorized("Could not authenticate user");
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(RegisterModel input)
    {
        if (input is null) throw new ArgumentNullException(nameof(input));

        var user = new IdentityUser
        {
            Email = input.Email,
            NormalizedEmail = input.Email.ToUpper(CultureInfo.InvariantCulture),
            UserName = input.UserName,
            NormalizedUserName = input.UserName.ToUpper(CultureInfo.InvariantCulture)
        };
        var result = await _userManager.CreateAsync(user, input.Password);
        if (!result.Succeeded) return Conflict();
        await _signInManager.SignInAsync(user, false);
        return Ok();
    }

    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    private string GenerateToken()
    {
        return "";
    }
}*/

