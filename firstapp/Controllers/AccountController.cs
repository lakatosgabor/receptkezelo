using firstapp.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDTO model)
    {
        if (ModelState.IsValid)
        {
            if (_userManager.Users.Any(u => u.UserName == model.UserName || u.Email == model.Email))
            {
                throw new ApplicationException("Username/Email already exists!");
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                City = model.City,
                Country = model.Country,
                ProfilePictureUrl = model.ProfilePictureUrl
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Sikeres regisztráció, lehet végrehajtani további tevékenységeket
                return Ok(new { Message = "Sikeres regisztráció" });
            }

            return BadRequest(result.Errors);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (signInResult.Succeeded)
        {
            // Sikeres bejelentkezés
            return Ok(new { Message = "Sikeres bejelentkezés" });
        }
        else
        {
            return BadRequest("Bejelentkezés sikertelen");
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        // Sikeres kijelentkezés
        return Ok(new { Message = "Sikeres kijelentkezés" });
    }
}
