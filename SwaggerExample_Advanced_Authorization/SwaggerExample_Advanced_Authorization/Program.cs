using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SwaggerExample_Advanced_Authorization.Filters;
using SwaggerExample_Advanced_Authorization.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region Swagger
builder.Services.AddSwaggerGen(options => {
  // using System.Reflection;
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
  options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT驗證描述"
  });
  options.OperationFilter<AuthorizeCheckOperationFilter>();
}); 
#endregion

#region JWT
//清除預設映射
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
//註冊JwtHelper
builder.Services.AddSingleton<JwtHelper>();
//使用選項模式註冊
builder.Services.Configure<JwtSettingsOptions>(
    builder.Configuration.GetSection("JwtSettings"));
//設定認證方式
builder.Services
  //使用bearer token方式認證並且token用jwt格式
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
      // 可以讓[Authorize]判斷角色
      RoleClaimType = "roles",
      // 預設會認證發行人
      ValidateIssuer = true,
      ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),
      // 不認證使用者
      ValidateAudience = false,
      // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
      ValidateIssuerSigningKey = true,
      // 簽章所使用的key
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SignKey")))
    };
  }); 
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//認證
app.UseAuthentication();
//授權
app.UseAuthorization();

app.MapControllers();

app.Run();
