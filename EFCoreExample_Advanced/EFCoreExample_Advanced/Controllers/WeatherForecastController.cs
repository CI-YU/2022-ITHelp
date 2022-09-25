using EFCoreExample_Advanced.DBContext;
using EFCoreExample_Advanced.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreExample_Advanced.Controllers {
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
    [HttpGet("QueryAsync")]
    public async Task<IActionResult> QueryAsync() {
      var Result = await _context.Students.ToListAsync();
      return Ok(Result);
    }
    [HttpGet("QueryFirstOrDefaultAsync")]
    public async Task<IActionResult> QueryFirstOrDefaultAsync() {
      var Result = await _context.Students.FirstOrDefaultAsync();
      return Ok(Result);
    }
    [HttpGet("TransactionsAsync")]
    public async Task<IActionResult> TransactionsAsync() {
      using var trans = _context.Database.BeginTransaction();
      var data = new Student() { Name = "BillHuang", Age = 20 };
      _context.Students.Add(data);
      await _context.SaveChangesAsync();
      var Result = await _context.Students.FirstOrDefaultAsync();
      trans.Commit();
      return Ok(Result);
    }
  }
}