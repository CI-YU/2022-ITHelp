using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace JsonExample_Advanced_Deserialize.Controllers {
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

    //[HttpGet(Name = "GetWeatherForecast")]
    //public IEnumerable<WeatherForecast> Get() {
    //  return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
    //    Date = DateTime.Now.AddDays(index),
    //    TemperatureC = Random.Shared.Next(-20, 55),
    //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //  })
    //  .ToArray();
    //}
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <returns></returns>
    [HttpGet("JsonDeserialize")]
    public ActionResult JsonDeserialize() {
      var options = new JsonSerializerOptions {
        PropertyNamingPolicy = null,
      };
      var jsonString = @"{""Name"":""中文名"",""Age"":18,""TemperatureCelsius"":52}";
      var Result = JsonSerializer.Deserialize<TestClass>(jsonString,options);
      return Ok(Result);
    }
    public class TestClass {
      public string Name { get; set; }
      public int Age { get; set; }
      public int TemperatureCelsius { get; set; }
    }
  }
}