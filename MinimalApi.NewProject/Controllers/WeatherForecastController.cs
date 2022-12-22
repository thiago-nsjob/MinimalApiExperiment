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
[Authorize]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration _configuration;
    
    public WeatherForecastController(ILogger<WeatherForecastController> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    /// <summary>
    /// Get Weather Data
    /// </summary>
    /// <returns></returns>
    [HttpGet("",Name = "GetAllForecast")]
    public async Task<IActionResult> GetAll()
    {
      var result =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
      return await Task.FromResult(Ok(result));
    }
    
}