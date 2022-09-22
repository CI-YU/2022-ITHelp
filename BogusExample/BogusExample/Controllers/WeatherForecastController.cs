using Bogus;
using Microsoft.AspNetCore.Mvc;
using static Bogus.DataSets.Name;

namespace BogusExample.Controllers {
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
    [HttpGet("Test")]
    public List<User> Test() {
      //可限制隨機值為定值
      //Randomizer.Seed = new Random(8675307);
      //建立一個假的貨品陣列
      var fruit = new[] { "apple", "banana", "orange", "strawberry", "kiwi" };
      //預設訂單編號為0
      var orderIds = 0;
      //預設取得英文資料
      var testOrders = new Faker<Order>()
        //強制所有屬性都要有規則存在，預設為false
        .StrictMode(true)
        //OrderId is deterministic
        .RuleFor(o => o.OrderId, f => orderIds++)
        //從自訂陣列隨機取值
        .RuleFor(o => o.Item, f => f.PickRandom(fruit))
        //從1-10隨機取值
        .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
        //從1-100隨機取值，並有20%機會為NULL
        .RuleFor(o => o.LotNumber, f => f.Random.Int(0, 100).OrNull(f, .2f));
      //預設使用者編號為0
      var userIds = 0;
      var testUsers = new Faker<User>()
        //使用需要初始化的類別
        .CustomInstantiator(f => new User(userIds++, f.Random.Replace("(##)###-####")))

        //從列舉中隨機取值(Gender為Bogus內建)
        .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())

        //使用內建的生成器
        .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
        .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
        .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
        .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
        .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
        .RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")

        //可使用非Bogus的方法，建立一個新的GUID
        .RuleFor(u => u.CartId, f => Guid.NewGuid())
        //可使用複合屬性
        .RuleFor(u => u.FullName, (f, u) => $"{u.FirstName} {u.LastName}")
        //複雜的集合也可以使用，並重複產生5個訂單的陣列
        .RuleFor(u => u.Orders, f => testOrders.Generate(5).ToList())
        //最後結束後可以執行特定動作
        .FinishWith((f, u) =>
        {
          Console.WriteLine("User Created! Id={0}", u.Id);
        });
      //產生3個使用者
      var user = testUsers.Generate(3);
      return user;
    }
    public class User {
      public User(int v1, string v2) {
        Id = v1;
        SSN = v2;
      }
      public int Id { get; set; }
      public Gender Gender { get; set; }
      public string SSN { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Avatar { get; set; }
      public string UserName { get; set; }
      public string Email { get; set; }
      public string SomethingUnique { get; set; }
      public Guid CartId { get; set; }
      public string FullName { get; set; }
      public List<Order> Orders { get; set; }
    }

    public class Order {
      public int OrderId { get; set; }
      public string Item { get; set; }
      public int Quantity { get; set; }
      public int? LotNumber { get; set; }
    }
  }
}