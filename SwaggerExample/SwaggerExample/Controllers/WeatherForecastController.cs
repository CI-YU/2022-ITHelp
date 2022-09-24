using Microsoft.AspNetCore.Mvc;

namespace SwaggerExample.Controllers {
  [ApiController]
  [Route("[controller]/[action]")]
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
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }

    /// <summary>
    /// 透過id取得相關資料
    /// </summary>
    /// <param name="id">流水號</param>
    /// <returns></returns>
    [HttpGet(Name = "GetSomething")]
    public string GetSomething(int id) {
      return "This is Return Value";
    }
  }
}