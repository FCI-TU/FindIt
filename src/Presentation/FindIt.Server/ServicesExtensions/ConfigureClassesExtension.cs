using FindIt.Application.Settings;
using FindIt.Persistence.Settings;

namespace FindIt.Server.ServicesExtensions
{
    public static class ConfigureClassesExtension
    {
        public static IServiceCollection ConfigureAppSettingsData(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtData>(configuration.GetSection("JWT"));
            services.Configure<DatabaseConnections>(configuration.GetSection("ConnectionStrings"));
            return services;
        }
    }
}
