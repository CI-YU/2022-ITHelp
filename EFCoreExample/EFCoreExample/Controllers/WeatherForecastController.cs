using EFCoreExample.DBContext;
using EFCoreExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreExample.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly EFCoreContext _context;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, EFCoreContext context) {
      _logger = logger;
      _context = context;
    }

    //[HttpGet(Name = "GetWeatherForecast")]
    //public IEnumerable<WeatherForecast> Get() {
    //  return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
    //    Date = DateTime.Now.AddDays(index),
    //    TemperatureC = Random.Shared.Next(-20, 55),
    //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //  })
    //  .ToArray();
    //}
    [HttpGet("InsertAsync")]
    public async Task<IActionResult> InsertAsync() {
      var data = new Student() { Name = "BillHuang", Age = 20 };
      _context.Students.Add(data);
      return Ok(await _context.SaveChangesAsync());
    }
  }
}