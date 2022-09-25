using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Text;

namespace DapperExample_Advanced.Controllers {
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
    /// ExecuteAsync可用於insert、delete、update
    /// </summary>
    /// <returns></returns>
    [HttpGet("ExcuteAsync")]
    public async Task<IActionResult> ExcuteAsync() {
      //建立SQLite連線
      using var conn = new SqliteConnection("Data Source=Product.sqlite");
      var SQL = new StringBuilder();
      //初始化SQLite
      await InitSqliteAsync();
      SQL.Append("INSERT INTO Product (Name, Age) VALUES (@Name, @Age);");
      DynamicParameters parameters = new();
      parameters.Add("Name", "BillHuang");
      parameters.Add("Age", 20);
      var Result = await conn.ExecuteAsync(SQL.ToString(), parameters);
      return Ok(Result);
    }
    /// <summary>
    /// QueryAsync可用於select
    /// </summary>
    /// <returns></returns>
    [HttpGet("QueryAsync")]
    public async Task<IActionResult> QueryAsync() {
      //建立SQLite連線
      using var conn = new SqliteConnection("Data Source=Product.sqlite");
      var SQL = new StringBuilder();
      //初始化SQLite
      await InitSqliteAsync();
      SQL.Append("select * from Product");
      var Result = await conn.QueryAsync<Product>(SQL.ToString());
      return Ok(Result);
    }
    /// <summary>
    /// 取得select第一筆
    /// </summary>
    /// <returns></returns>
    [HttpGet("QueryFirstOrDefaultAsync")]
    public async Task<IActionResult> QueryFirstOrDefaultAsync() {
      //建立SQLite連線
      using var conn = new SqliteConnection("Data Source=Product.sqlite");
      var SQL = new StringBuilder();
      //初始化SQLite
      await InitSqliteAsync();
      SQL.Append("select * from Product");
      var Result = await conn.QueryFirstOrDefaultAsync<Product>(SQL.ToString());
      if (Result is not null) {
        return Ok(Result);
      }
      return Ok(Result);
    }
    /// <summary>
    /// 交易機制，簡單說就是全部成功才算成功，不然就全部取消。
    /// </summary>
    /// <returns></returns>
    [HttpGet("TransactionsAsync")]
    public async Task<IActionResult> TransactionsAsync() {
      using var conn = new SqliteConnection("Data Source=Product.sqlite");
      //開啟連線，前面沒有這行是因為在在執行語法時(Execute、Query)會自動檢查是否連接資料庫
      conn.Open();
      //開始資料庫交易
      var trans = conn.BeginTransaction();
      var SQL = new StringBuilder();
      //初始化SQLite
      await InitSqliteAsync();
      SQL.Append("INSERT INTO Product (Name, Age) VALUES (@Name, @Age);");
      DynamicParameters parameters = new();
      parameters.Add("Name", "BillHuang");
      parameters.Add("Age", 20);
      //執行完並不會真的異動資料
      await conn.ExecuteAsync(SQL.ToString(), parameters, trans);
      SQL.Clear();
      SQL.Append("select * from Product");
      var Result = await conn.QueryFirstOrDefaultAsync<Product>(SQL.ToString(), trans);
      //當程式執行到Commit才是真的執行成功。
      trans.Commit();
      return Ok();
    }
    /// <summary>
    /// 初始化SQLite
    /// </summary>
    /// <returns></returns>
    private static async Task InitSqliteAsync() {
      //建立SQLite連線
      using var conn = new SqliteConnection("Data Source=Product.sqlite");
      var SQL = new StringBuilder();
      //判斷是否有Product.sqlite檔案
      if (!System.IO.File.Exists(@".\Product.sqlite")) {
        //新增一張表，就會建立.sqlite檔案
        SQL.Append("CREATE TABLE Product( \n");
        SQL.Append("Id INTEGER PRIMARY KEY AUTOINCREMENT, \n");
        SQL.Append("Name VARCHAR(32) NOT NULL, \n");
        SQL.Append("Age INTEGER) \n");
        //執行sql語法
        await conn.ExecuteAsync(SQL.ToString());
      }
      //Task不建議使用void，當不需要回傳值時會改用Task.CompletedTask說明已經完成，可以下一個步驟了。
      await Task.CompletedTask;
    }
    public class Product {
      public int Id { get; set; }
      //Name預設值為Billhuang，與以前建構子的寫法一樣，如下方寫法
      //public Product(){Name="BillHuang";}
      public string Name { get; set; } = "BillHuang";
      public int Age { get; set; }
    }
  }

}