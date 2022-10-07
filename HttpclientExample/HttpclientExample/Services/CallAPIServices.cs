namespace HttpclientExample.Services {
  public class CallAPIServices {
    readonly IHttpClientFactory _httpClientFactory;
    public CallAPIServices(IHttpClientFactory httpClientFactory) {
      _httpClientFactory = httpClientFactory;
    }

    public async Task<string> Get() {
      var client = _httpClientFactory.CreateClient();
      var response = await client.GetAsync("https://example.com");
      if (response.IsSuccessStatusCode) {
        var responseString = await response.Content.ReadAsStringAsync();
        return responseString;
      }
      return "";
    }
  }
}
