using Serilog;
//第一階段初始化
 // var builder = WebApplication.CreateBuilder(args);未使用二階段參數化，builder會跑到try外面
Log.Logger = new LoggerConfiguration()
  //.ReadFrom.Configuration(builder.Configuration)
  .CreateBootstrapLogger();
try {
  var builder = WebApplication.CreateBuilder(args);
  // Add services to the container.

  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();
  //第二階段初始化可以取得appsetting的內容，如不使用第二階段初始化，
  //會需要將 var builder宣告式會移出try(如上方註解處)，就會有風險未捕獲builder的錯誤
  builder.Host.UseSerilog(
      (hostingContext, services, loggerConfiguration) => {
        //使用appsetting
        loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
      });
  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
  }

  app.UseHttpsRedirection();
  //紀錄每個Request的資料，須注意要放在讀取靜態檔案(app.UseStaticFiles())後面，因為靜態檔案的狀態通常不需要紀錄資訊
  app.UseSerilogRequestLogging();
  app.UseAuthorization();

  app.MapControllers();

  app.Run();
  return 0;
} catch (Exception ex) {
  Log.Fatal(ex.Message, "Host terminated unexpectedly");
  return 1;
} finally { Log.CloseAndFlush(); }