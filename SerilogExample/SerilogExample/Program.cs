using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    //Serilog要寫入的最低等級為Information
    .MinimumLevel.Information()
    //Microsoft.AspNetCore開頭的類別等極為warning
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    //寫log到Logs資料夾的log.txt檔案中，並且以天為單位做檔案分割
    .WriteTo.File("./Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
try {
  Log.Information("Starting web host");
  var builder = WebApplication.CreateBuilder(args);
  builder.Services.AddControllers();
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();
  //controller可以使用ILogger介面來寫入log紀錄
  builder.Host.UseSerilog();
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
  return 0;
} catch (Exception ex) {
  Log.Fatal(ex, "Host terminated unexpectedly");
  return 1;
} finally { Log.CloseAndFlush(); }
