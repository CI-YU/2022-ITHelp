using Microsoft.Extensions.Logging;
using Moq;
using MoqExample.Controllers;
using Xunit;

namespace MoqExample.Tests.Controllers {
  public class WeatherForecastControllerTests {
    [Fact]
    public void Get() {
      //Arrange
      var MockLogger = new Mock<ILogger<WeatherForecastController>>();
      var Controllers = new WeatherForecastController(MockLogger.Object);
      //Act
      var Results = Controllers.Get();
      //Assert
      Assert.NotNull(Results);
      Assert.Equal(5, Results.Count());
    }
  }
}
