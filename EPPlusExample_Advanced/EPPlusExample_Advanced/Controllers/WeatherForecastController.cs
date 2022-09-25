using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Drawing.Chart.Style;

namespace EPPlusExample_Advanced.Controllers {
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
      //建立excel所有操作的實例
      using ExcelPackage excelPackage = new();
      var ws = excelPackage.Workbook.Worksheets.Add("第一頁");
      Random Random = new Random();
      //ws.Cells[上下(row),左右(col)]
      ws.Cells[1, 2].Value = "第一季";
      ws.Cells[1, 3].Value = "第二季";
      ws.Cells[1, 4].Value = "第三季";
      ws.Cells[1, 5].Value = "第四季";
      ws.Cells[2, 1].Value = "A組";
      ws.Cells[3, 1].Value = "B組";
      ws.Cells[4, 1].Value = "C組";
      ws.Cells[5, 1].Value = "D組";
      for (int i = 2; i <= 5; i++) {
        for (int j = 2; j <= 5; j++) {
          ws.Cells[i, j].Value = Random.Next(70, 150);
        }
      }
      //建立長條圖
      var BarChart = ws.Drawings.AddBarChart("BarChart", eBarChartType.ColumnClustered);
      //長條圖名稱
      BarChart.Title.Text = "年度季報表";
      //長條圖的位置
      BarChart.SetPosition(6, 0, 6, 0);
      //長條圖大小
      BarChart.SetSize(400, 400);
      //第一個顏色長條圖BarChart.Series.Add(數據區間，x軸名稱區間)=>數據區間從(2,2)到(2,5)，X軸名稱(第一季、第二季、第三季、第四季)
      var Ateam = BarChart.Series.Add(ExcelCellBase.GetAddress(2, 2, 2, 5), ExcelCellBase.GetAddress(1, 2, 1, 5));
      //第一條顏色的名稱(A組)
      Ateam.Header = ws.Cells[2, 1].Text;
      var Bteam = BarChart.Series.Add(ExcelCellBase.GetAddress(3, 2, 3, 5), ExcelCellBase.GetAddress(1, 2, 1, 5));
      Bteam.Header = ws.Cells[3, 1].Text;
      var Cteam = BarChart.Series.Add(ExcelCellBase.GetAddress(4, 2, 4, 5), ExcelCellBase.GetAddress(1, 2, 1, 5));
      Cteam.Header = ws.Cells[4, 1].Text;
      var Dteam = BarChart.Series.Add(ExcelCellBase.GetAddress(5, 2, 5, 5), ExcelCellBase.GetAddress(1, 2, 1, 5));
      Dteam.Header = ws.Cells[5, 1].Text;
      //樣式使用1
      BarChart.StyleManager.SetChartStyle(ePresetChartStyle.HistogramChartStyle1);

      //將檔案匯出
      return File(excelPackage.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "製作長條圖");
    }
  }
}