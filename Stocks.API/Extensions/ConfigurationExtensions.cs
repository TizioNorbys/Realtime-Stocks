using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Stocks.API.Extensions;

public static class ConfigurationExtensions
{
    public static AuthenticationBuilder AddJwtBearerConfiguration(this AuthenticationBuilder services, IConfiguration configuration)
    {
        return services.AddJwtBearer(options =>
        {
            var jwtSection = configuration.GetSection("Jwt");

            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSection.GetValue<string>("Issuer"),
                ValidAudience = jwtSection.GetValue<string>("Audience"),
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(jwtSection.GetValue<string>("SecretKey")!))
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["acccess_token"];
                    var path = context.HttpContext.Request.Path;

                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/stocks-hub"))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };
        });
    }
}