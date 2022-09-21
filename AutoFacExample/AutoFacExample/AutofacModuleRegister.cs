using Autofac;

namespace AutoFacExample {
  public class AutofacModuleRegister : Module {
    protected override void Load(ContainerBuilder builder) {
      builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces();
    }
  }
}
