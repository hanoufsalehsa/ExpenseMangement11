// UserController.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseMangement11.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using ExpenseMangement11.Services;






[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    // User registration, login, and logout endpoints...
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly UserService _userService;


    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new User
        {
            Username = model.Username,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password,
            
            // Add other user-related properties as needed

            
        };
        
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully." });
            }

            return BadRequest(new { Message = "User registration failed.", Errors = result.Errors });
        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                // Add other claims as needed
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds

            );

            await _signInManager.SignInAsync(user, isPersistent: false);

            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        
        return Unauthorized(new { Message = "Invalid username or password." });
        
    }

    //handling JWT token expiration, refresh tokens

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
    {
        var principal = GetPrincipalFromExpiredToken(model.Token);
        var username = principal.Identity.Name; // You can get additional user claims here if needed

        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return Unauthorized(new { Message = "User not found." });
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]));
    
        var newToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims: principal.Claims,
            expires: expires,
            signingCredentials: creds
            
        );

        return Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(newToken),
            ExpiresInMinutes = Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]),
            ExpiryDateTime = expires
        });
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false // This allows token expiration to be ignored
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }


    [HttpPost("logout")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Message = "User logged out successfully." });
    }


 
    //CRUD
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _userService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public ActionResult AddUser([FromBody] User user)
    {
        _userService.AddUser(user);
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser(int id, [FromBody] User user)
    {
        var existingUser = _userService.GetUser(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        user.UserID = id;
        _userService.UpdateUser(user);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        var existingUser = _userService.GetUser(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        _userService.DeleteUser(id);
        return Ok();
    }


}
