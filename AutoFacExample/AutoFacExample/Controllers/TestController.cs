using AutoFacExample.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoFacExample.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class TestController : ControllerBase {
    private readonly ITest _test;
    public TestController(ITest test) {
      _test = test;
    }
    [HttpGet("GetName")]
    public string Get(string id) { 
    return _test.GetName(id);
    }
  }
}
