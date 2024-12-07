using FindIt.Application.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FindIt.Application;
public static class ServiceExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services, JwtData jwtData)
    {

        services.AddAuthentication(authenticationOptions =>
        {
            authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            authenticationOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtBearerOptions =>
        {
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = jwtData.ValidAudience,
                ValidateIssuer = true,
                ValidIssuer = jwtData.ValidIssuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtData.SecretKey)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
        });

        return services;
    }
}
