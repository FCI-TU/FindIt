using FindIt.Application;
using FindIt.Application.Settings;
using FindIt.Persistence;
using FindIt.Persistence.Settings;
using FindIt.Server.ServicesExtensions;
using Microsoft.Extensions.Options;

namespace FindIt.Server
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddApplicationDependencies(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;
            var environment = builder.Environment;

            services.ConfigureAppSettingsData(configuration);

            var servicesProvider = services.BuildServiceProvider();

            var jwtData = servicesProvider.GetRequiredService<IOptions<JwtData>>().Value;
            var databaseConnections = servicesProvider.GetRequiredService<IOptions<DatabaseConnections>>().Value;

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSwaggerServices();

            services.AddApiVersioningConfigurations();

            services.AddStoreContext(databaseConnections.FindItDb);


            services.AddIdentityConfigurations();

            services.AddAuthenticationService(jwtData);

            services.AddApplicationServices();

            return builder;
        }
    }
}
