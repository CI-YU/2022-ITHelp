using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CallAPIServices.Tests.Services {
  public class CallAPIServicesTests {
    [Fact]
    public async Task CallAPIServices_Get_HttpClient_Success() {
      var mockFactory = new Mock<IHttpClientFactory>();
      HttpResponseMessage result = new HttpResponseMessage() {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent("{'account':'bill','age':18}")
      };
      var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
      mockHttpMessageHandler.Protected()
          .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
          .ReturnsAsync(result);

      var client = new HttpClient(mockHttpMessageHandler.Object);
      mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

      var services = new HttpclientExample.Services.CallAPIServices(mockFactory.Object);

      var response = await services.Get();
      Assert.NotNull(response);
      Assert.Equal("{'account':'bill','age':18}", response);
    }
  }
}
