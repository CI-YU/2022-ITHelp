using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwaggerExample_Advanced_Authorization.Helpers;

namespace SwaggerExample_Advanced_Authorization.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly JwtHelper _jwtHelpers;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, JwtHelper jwtHelpers) {
      _logger = logger;
      _jwtHelpers = jwtHelpers;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get() {
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }
    [HttpGet("Login"), AllowAnonymous]
    public ActionResult<string> Login(string username , string password) {
      var token = _jwtHelpers.GenerateToken(username);
      return Ok(token);
    }
    [HttpGet("username"), Authorize(Roles = "admin")]
    public ActionResult<string> Username() {
      return Ok(User.Identity?.Name);
    }
  }
}