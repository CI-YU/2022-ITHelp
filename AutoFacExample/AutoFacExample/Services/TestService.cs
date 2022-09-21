using AutoFacExample.Services.Interface;

namespace AutoFacExample.Services {
  public class TestService : ITest {
    public string GetName(string id) {
      return $"{id}:Bill";
    }
  }
}
