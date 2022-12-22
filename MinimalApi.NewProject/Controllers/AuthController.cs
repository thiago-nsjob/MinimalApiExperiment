

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MinimalApiExperiment.Controllers;
/// <summary>
/// WeatherForecast App
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Create Dev Token
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("token",Name = "Generate Token")]
    public async Task<IActionResult> GetToken()
    {
          // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("user", "dev") }),
            Expires = DateTime.UtcNow.AddDays(7),
            Audience = "Dev",
            Issuer = "Localhost",
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Security:JwtSecret"]))
                , SecurityAlgorithms.HmacSha256Signature
                )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var result = tokenHandler.WriteToken(token);
        return await Task.FromResult(Ok(result));
    }
}