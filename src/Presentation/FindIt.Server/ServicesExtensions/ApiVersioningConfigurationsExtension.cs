using Asp.Versioning;

namespace FindIt.Server.ServicesExtensions
{
    public static class ApiVersioningConfigurationsExtension
    {
        public static IServiceCollection AddApiVersioningConfigurations(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'V";
                    options.SubstituteApiVersionInUrl = true;
                });
            return services;
        }
    }
}
