using System.Net;
using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO.Error;
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
    
    //TODO: change error messages
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
            return NotFound("User/Password problem 1");
        }
        
        //verify username and password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Login failed, password problem for user {}", loginData.Email);
            await Task.Delay(_random.Next(100,1000));
            return NotFound("User/Password problem 2");
        }
        
        //get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", loginData.Email);
            await Task.Delay(_random.Next(100,1000));
            return NotFound("User/Password problem 3");
        }

        //generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireInDays"))
        );

        // can add additional data to jwt response
        var res = new JwtResponse()
        {
            Token = jwt
        };
        return Ok(res);
    }

    public async Task<ActionResult<JwtResponse>> Register(Register registrationData)
    {
        //verify user
        var appUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User with email {} is already registered", registrationData.Email);
            var errorResponse = new RestApiErrorResponse(){
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "App error",
                Status =  HttpStatusCode.BadRequest,
                TraceId = HttpContext.TraceIdentifier,
            };
            errorResponse.Errors["email"] = new List<string>()
            {
                "Email already registered"
            };
            return BadRequest(errorResponse);
        }

        appUser = new AppUser()
        {
            Email = registrationData.Email,
            UserName = registrationData.Email
        };

        //create user (system creates)
        var result = await _userManager.CreateAsync(appUser, registrationData.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        
        //get full user (get from system created)
        appUser = await _userManager.FindByEmailAsync(appUser.Email);
        if (appUser == null)
        {
            _logger.LogWarning("Useer {} not found after registration", registrationData.Email);
            return BadRequest("Cant create user!");
        }
        
        //get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", registrationData.Email);
            return NotFound("User/Password problem 3");
        }

        //generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireInDays"))
        );

        // can add additional data to jwt response
        var res = new JwtResponse()
        {
            Token = jwt
        };
        return Ok(res);
    }
}