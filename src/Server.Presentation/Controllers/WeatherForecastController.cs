#region

using Microsoft.AspNetCore.Mvc;
using Server.Contracts.Exceptions;
using Server.Domain.Scaffolded;

#endregion

namespace Server.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet]
    [Route("LogExceptionTest")]
    public IActionResult GetException()
    {
        throw new EntityNotFoundByIdException<User>(9);
    }

    [HttpGet]
    [Route("FATAL")]
    public IActionResult FATAL()
    {
        throw new Exception("Test unhandled exception logging");
        return Ok();
    }
}