using Microsoft.AspNetCore.Mvc;

namespace SerilogExample_Advanced.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger) {
      _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get() {
      var StudentObject = new Student() { Id = 4, Name = "Bill", Age = 20 };
      var position = new { Latitude = 25, Longitude = 134 };
      _logger.LogError("Error Value: {@StudentObject}", StudentObject);
      _logger.LogError("Error Value: {@position}", position);
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }
    public class Student {
      public int Id { get; set; }
      public string Name { get; set; }
      public int Age { get; set; }
    }
  }
}