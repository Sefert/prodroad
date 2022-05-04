using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using DAL.App.EF;
using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO.Error;
using WebApp.DTO.Identity;

namespace WebApp.ApiControllers.Identity;

[ApiController]
[ApiVersion( "1.0" )]
[Produces( "application/json" )]
[Consumes( "application/json" )]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
public class AccountController : ControllerBase
{

    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly Random _random = new();
    private readonly AppDbContext _context;

    public AccountController(SignInManager<AppUser> signInManager, 
            ILogger<AccountController> logger, 
            UserManager<AppUser> userManager, 
            IConfiguration configuration, 
            AppDbContext context)
    {
        _signInManager = signInManager;
        _logger = logger;
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
    }
    /// <summary>
    /// TODO: needs stuff here
    /// </summary>
    /// <param name="loginData"></param>
    /// <returns></returns>
    //TODO: change error messages
    [HttpPost] //Find out why actionresult. 
    [ProducesResponseType( typeof(JwtResponse), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JwtResponse>> LogIn([FromBody] Login loginData) //Frombody json body
    {
        //verify username
        var appUser = await _userManager.FindByEmailAsync(loginData.Email);
        RefreshToken refreshToken;
        if (appUser == null)
        {
            _logger.LogWarning("Login failed, email {} not found", loginData.Email);
            
            //Should do random delays in between different steps on failure
            await Task.Delay(_random.Next(100,1000));
            return NotFound("User/Password problem 1");
        }
        
        //verify username and password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (result.Succeeded)
        {
            //add RefreshToken to user
            refreshToken = new RefreshToken();
            if (appUser.RefreshTokens == null)
            {
                appUser.RefreshTokens = new List<RefreshToken>()
                {
                    refreshToken
                }; 
            }
            else
            {
                appUser.RefreshTokens.Add(refreshToken); 
            }
            await _context.SaveChangesAsync();
        }
        else
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
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );

        // can add additional data to jwt response
        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token
        };
        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<JwtResponse>> Register(Register registrationData)
    {
        //verify user
        var appUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User with email {} is already registered", registrationData.Email);

            var errorResponse = RequestResponse(HttpStatusCode.BadRequest);
            
            errorResponse.Errors["email"] = new List<string>()
            {
                "Email already registered!"
            };
            
            return BadRequest(errorResponse);
        }

        var refreshToken = new RefreshToken();
        
        appUser = new AppUser()
        {
            Email = registrationData.Email,
            UserName = registrationData.Email,
            RefreshTokens = new List<RefreshToken>()
            {
                refreshToken
            }
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
            _logger.LogWarning("User {} not found after registration!", registrationData.Email);
            var errorResponse = RequestResponse(HttpStatusCode.BadRequest);
            
            errorResponse.Errors["user"] = new List<string>()
            {
                "Cant create user!"
            };
            return BadRequest(errorResponse);
        }
        
        //get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", registrationData.Email);
            var errorResponse = RequestResponse(HttpStatusCode.NotFound);
            
            errorResponse.Errors["user"] = new List<string>()
            {
                "Claims problem!"
            };
            return NotFound(errorResponse);
        }

        //generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );

        // can add additional data to jwt response
        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token
        };
        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel)
    {
        //get user info from JWT
        JwtSecurityToken jwtToken;
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Token);
            if (jwtToken == null)
            {
                var errorResponse = RequestResponse(HttpStatusCode.BadRequest);
            
                errorResponse.Errors["token"] = new List<string>()
                {
                    "No token!"
                };
                return BadRequest(errorResponse);
            }
        }
        catch (Exception e)
        {
            var errorResponse = RequestResponse(HttpStatusCode.BadRequest);
            
            errorResponse.Errors["token"] = new List<string>()
            {
                $"Invalid JWT!:{e.Message}"
            };
            return BadRequest(errorResponse);
        }
        
        //validate token signature
        var userEmail = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            var errorResponse = RequestResponse(HttpStatusCode.BadRequest);
            
            errorResponse.Errors["token"] = new List<string>()
            {
                "No email in JWT"
            };
            return BadRequest(errorResponse);
        }

        //get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            var errorResponse = RequestResponse(HttpStatusCode.NotFound);
            
            errorResponse.Errors["user"] = new List<string>()
            {
                "User not found!"
            };
            return NotFound("User not found!");
        }
        
        //compare refresh tokens
        
        await _context.Entry(appUser).Collection(u => u.RefreshTokens!)
            .Query()
            .Where(x => (x.Token ==refreshTokenModel.RefreshToken && x.TokenExpirationDateTime > DateTime.UtcNow) ||
                       (x.PreviousToken == refreshTokenModel.RefreshToken || x.PreviousTokenExpirationDateTime > DateTime.UtcNow))
            .ToListAsync();
        
        if (appUser.RefreshTokens == null)
        {
            return Problem("Refresh Token not found","",500);
        }
        
        if (appUser.RefreshTokens == null)
        {
            return Problem("More then one refresh tokens found","",500);
        }
        
        //generate new JWT
        //get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", userEmail);
            var errorResponse = RequestResponse(HttpStatusCode.NotFound);
            
            errorResponse.Errors["user"] = new List<string>()
            {
                "User not found!"
            };
            return NotFound(errorResponse);
        }

        //generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );

        //generate new refresh token
        //save new refresh token, move old one to prev, update expiration
        var refreshToken = appUser.RefreshTokens.First();
        if (refreshToken.Token == refreshTokenModel.RefreshToken)
        {
            refreshToken.PreviousToken = refreshToken.Token;
            refreshToken.PreviousTokenExpirationDateTime= DateTime.UtcNow.AddMinutes(1);

            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.TokenExpirationDateTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();
        }
        
        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token
        };

        return Ok(res);
    }

    private RestApiErrorResponse RequestResponse(HttpStatusCode code)
    {
        string type;

        switch(code) 
        {
            case HttpStatusCode.BadRequest:
                type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                break;
            case HttpStatusCode.NotFound:
                type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
                break;
        };
        
        var errorResponse = new RestApiErrorResponse(){
            Type = type,
            Title = "APP error",
            Status =  code,
            TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
        };

        return errorResponse;
    }
}