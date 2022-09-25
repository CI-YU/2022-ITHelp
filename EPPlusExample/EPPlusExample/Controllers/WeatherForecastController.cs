using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace EPPlusExample.Controllers {
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

    [HttpGet(Name = "Import")]
    public ActionResult ImportExcel() {
      using ExcelPackage excelPackage = new();
      excelPackage.Workbook.Properties.Author = "Bill Huang";
      excelPackage.Workbook.Properties.Title = "範例檔案";
      excelPackage.Workbook.Properties.Created = DateTime.Now;

      ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("第一頁");
      int i = 1;
      foreach (var c in Summaries) {
        worksheet.Cells[i,1].Value = c;
        i++;
      }
      return File(excelPackage.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "excel檔案預設名稱");
    }
  }
}