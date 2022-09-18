using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapperExample.Models.DbModel;
using AutoMapperExample.Models.ViewModel;
using AutoMapper;

namespace AutoMapperExample.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class ExampleController : ControllerBase {

    private readonly IMapper _mapper;
    public ExampleController(IMapper mapper) {
      _mapper = mapper;
    }
    [HttpGet("Index")]
    public IEnumerable<ViewModel> Index() {
      var DbModel = new List<DbModel>();
      DbModel.Add(new DbModel() { Id = 1, Name = "Bill", Age = 18, CreatedDate = DateTime.Now });
      DbModel.Add(new DbModel() { Id = 1, Name = "CI-YU", Age = 20, CreatedDate = DateTime.Now });
      DbModel.Add(new DbModel() { Id = 1, Name = "Bill Huang", Age = 22, CreatedDate = DateTime.Now });

      var map = _mapper.Map<IEnumerable<ViewModel>>(DbModel);
      return map;
    }
  }
}
