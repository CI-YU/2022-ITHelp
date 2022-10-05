using NLog;
using NLog.Web;
var logger = LogManager.Setup()
  .LoadConfigurationFromAppSettings()
  .GetCurrentClassLogger();
try {
  logger.Debug("init main");
  var builder = WebApplication.CreateBuilder(args);

  // Add services to the container.
  builder.Logging.ClearProviders();
  builder.Host.UseNLog();
  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
  }

  app.UseHttpsRedirection();

  app.UseAuthorization();

  app.MapControllers();

  app.Run();
} catch (Exception exception) {
  // NLog: catch setup errors
  logger.Error(exception, "Stopped program because of exception");
  throw;
} finally {
  // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
  LogManager.Shutdown();
}
