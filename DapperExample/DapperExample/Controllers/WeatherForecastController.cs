using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Text;

namespace DapperExample.Controllers {
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
    /// 檢查有沒有sqlite檔案，沒有就新增，並增加一筆資料
    /// </summary>
    /// <returns></returns>
    [HttpGet("InsertAsync")]
    public async Task<IActionResult> InsertAsync() {
      using var conn = new SqliteConnection("Data Source=Product.sqlite");
      var SQL = new StringBuilder();
      if (!System.IO.File.Exists(@".\Product.sqlite")) {
        SQL.Append("CREATE TABLE Product( \n");
        SQL.Append("Id INTEGER PRIMARY KEY AUTOINCREMENT, \n");
        SQL.Append("Name VARCHAR(32) NOT NULL, \n");
        SQL.Append("Age INTEGER) \n");
        await conn.ExecuteAsync(SQL.ToString());
        SQL.Clear();
      }
      SQL.Append("INSERT INTO Product (Name, Age) VALUES (@Name, @Age);");
      DynamicParameters parameters = new();
      parameters.Add("Name", "BillHuang");
      parameters.Add("Age", 20);
      var Result = await conn.ExecuteAsync(SQL.ToString(), parameters);
      return Ok(Result);
    }
    /// <summary>
    /// 取得Product所有資料
    /// </summary>
    /// <returns></returns>
    [HttpGet("SelectAsync")]
    public async Task<IActionResult> SelectAsync() {

      using var conn = new SqliteConnection("Data Source=Product.sqlite");
      var SQL = new StringBuilder();
      SQL.Append("select * from Product");
      var Result = await conn.QueryAsync<Product>(SQL.ToString());
      return Ok(Result);
    }
    public class Product {
      public int Id { get; set; }
      public string Name { get; set; } = "BillHuang";
      public int Age { get; set; }
    }
  }
}