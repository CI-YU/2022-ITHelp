using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace JsonExample_Advanced.Controllers {
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
    /// 序列化
    /// </summary>
    /// <returns></returns>
    [HttpGet("JsonSerialize")]
    public ActionResult JsonSerialize() {
      var options = new JsonSerializerOptions {
        //美化輸出，會有空白字元
        WriteIndented = true,
        //將所有語言都不進行轉換
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
      };

      var Test = new TestClass() {
        Name = "中文名",
        Age = 18,
      };
      var Result = JsonSerializer.Serialize(Test, options);
      return Ok(Result);
    }

    public class TestClass {
      public string Name { get; set; }
      public int Age { get; set; }
    }
  }
}