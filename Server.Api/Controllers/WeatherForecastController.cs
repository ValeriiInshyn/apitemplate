using Microsoft.AspNetCore.Mvc;

namespace Server.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
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
        try
        {
            throw new Exception("Test exception logging");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "This is demo exception");
            return BadRequest();
        }
        return Ok();
    }

    [HttpGet]
    [Route("FATAL")]
    public IActionResult FATAL()
    {
        throw new Exception("Test unhandled exception logging");
        return Ok();
    }
}