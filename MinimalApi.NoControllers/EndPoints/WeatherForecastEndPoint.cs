using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MinimalApi.NoControllers.EndPoints;

//http://www.binaryintellect.net/articles/f3dcbb45-fa8b-4e12-b284-f0cd2e5b2dcf.aspx
public  class WeatherForecastEndPoint :IEndPointDefinition
{
    private readonly IConfiguration _configuration;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    //Constructor injection
    public WeatherForecastEndPoint(IConfiguration configuration )
    {
        _configuration = configuration;
    }

    public void MapRoutes(WebApplication app)
    {
        //group wide config, ex prefixes and authorize requirements
        var group = app.MapGroup("/api/WeatherForecast")
            .WithOpenApi()
            .RequireAuthorization();

        group.MapGet("/", Get);
        group.MapPost("/", GetToken);
    }

    
    [AllowAnonymous]
    public async Task<IResult> GetToken()
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
      
        return await Task.FromResult(Results.Ok(result));
    }
    
    //Metho injection
    public  IEnumerable<WeatherForecast> Get(
        [FromServices]ILogger logger)
    {
        logger.LogInformation("DI Worked!");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
    
}