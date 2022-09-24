using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace SwaggerExample_Advanced_Authorization.Helpers {

  public class JwtHelper {
    private readonly JwtSettingsOptions _settings;

    public JwtHelper(IOptionsMonitor<JwtSettingsOptions> settings) {
      _settings = settings.CurrentValue;
    }

    public string GenerateToken(string userName, int expireMinutes = 120) {
      var issuer = _settings.Issuer;
      var signKey = _settings.SignKey;

      var token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                      .WithSecret(signKey)
                      .AddClaim("roles", "admin")
                      .AddClaim("jti", Guid.NewGuid().ToString()) // JWT ID
                      .AddClaim("iss", issuer)
                      .AddClaim("sub", userName) // User.Identity.Name
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(expireMinutes).ToUnixTimeSeconds())
                      .AddClaim("nbf", DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                      .AddClaim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                      .AddClaim(ClaimTypes.Name, userName)
                      .Encode();
      return token;
    }
  }
  public class JwtSettingsOptions {
    public string Issuer { get; set; } = "";
    public string SignKey { get; set; } = "";
  }
}
