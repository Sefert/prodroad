using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO.Identity;

namespace WebApp.ApiControllers.Identity;

[Route("api/identity/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{

    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly Random _random = new Random();

    public AccountController(SignInManager<AppUser> signInManager, 
            ILogger<AccountController> logger, 
            UserManager<AppUser> userManager, 
            IConfiguration configuration)
    {
        _signInManager = signInManager;
        _logger = logger;
        _userManager = userManager;
        _configuration = configuration;
    }
    
    [HttpPost] //Find out why actionresult. 
    public async Task<ActionResult<JwtResponse>> LogIn([FromBody] Login loginData) //Frombody json body
    {
        //verify username
        var appUser = await _userManager.FindByEmailAsync(loginData.Email);
        if (appUser == null)
        {
            _logger.LogWarning("Login failed, email {} not found", loginData.Email);
            
            //Should do random delays in between different steps on failure
            await Task.Delay(_random.Next(100,1000));
            return NotFound("User/Password problem");
        }
        
        //verify username and password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (result.Succeeded)
        {
            _logger.LogWarning("Login failed, password problem for user {}", loginData.Email);
            await Task.Delay(_random.Next(100,1000));
            return NotFound("User/Password problem");
        }
        
        //get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", loginData.Email);
            await Task.Delay(_random.Next(100,1000));
            return NotFound("User/Password problem");
        }

        //generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireInDays"))
        );

        var res = new JwtResponse()
        {

        };
        return Ok(res);
    }
    
    
}