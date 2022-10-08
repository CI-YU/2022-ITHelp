using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangfireExample.Controllers {
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
      //單次立即執行
      BackgroundJob.Enqueue(() => Console.WriteLine("單次!"));
      //單次10秒後執行
      BackgroundJob.Schedule(() => Console.WriteLine("10秒後執行!"), TimeSpan.FromSeconds(10));
      //重複執行，預設為每天00:00啟動
      RecurringJob.AddOrUpdate(() => Console.WriteLine("重複執行！"), Cron.Daily);
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }
  }
}