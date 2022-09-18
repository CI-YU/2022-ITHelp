using AutoMapper;
using AutoMapperExample.Models.DbModel;
using AutoMapperExample.Models.ViewModel;

namespace AutoMapperExample.Mappings {
  public class ExampleMapping : Profile {
    public ExampleMapping() {
      CreateMap<DbModel, ViewModel>();
    }
  }
}
