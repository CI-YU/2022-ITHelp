using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<BenchmarkSampleAuto>();

[MemoryDiagnoser]
public class BenchmarkSampleAuto {

  private readonly List<DbModel> _data = new List<DbModel>();
  private readonly IMapper _mapper;
  public BenchmarkSampleAuto() {
    var config = new MapperConfiguration(cfg => {
      cfg.CreateMap<DbModel, ViewModel>();
    });
    _mapper = new Mapper(config);

    PrepareTestObjects();
  }
  private void PrepareTestObjects() {
    _data.Add(new DbModel() { Id = 1, Name = "Bill", Age = 18, CreatedDate = DateTime.Now });
    _data.Add(new DbModel() { Id = 1, Name = "CI-YU", Age = 20, CreatedDate = DateTime.Now });
    _data.Add(new DbModel() { Id = 1, Name = "Bill Huang", Age = 22, CreatedDate = DateTime.Now });
  }
  [Benchmark]
  public List<ViewModel> first() {
    return _mapper.Map<List<ViewModel>>(_data);
  }
  [Benchmark]
  public List<ViewModel> second() {
    var listModel = new List<ViewModel>();
    foreach (var c in _data) {
      listModel.Add(new ViewModel() { Id = c.Id, Name = c.Name, Age = c.Age });
    }
    return listModel;
  }
  [Benchmark]
  public List<ViewModel> third() {
    var listModel = new List<ViewModel>();
    listModel = _data.Select(c => new ViewModel() { Id = c.Id, Name = c.Name, Age = c.Age }).ToList();
    return listModel;
  }
}

public class DbModel {
  public int Id { get; set; }
  public string? Name { get; set; }
  public int Age { get; set; }
  public DateTime CreatedDate { get; set; }
}
public class ViewModel {
  public ViewModel() {
    Name = string.Empty;
  }
  public int Id { get; set; }
  public string Name { get; set; }
  public int Age { get; set; }
}