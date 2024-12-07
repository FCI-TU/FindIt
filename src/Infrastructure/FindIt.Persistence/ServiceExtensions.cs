using FindIt.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FindIt.Persistence;
public static class ServiceExtensions
{
    public static IServiceCollection AddStoreContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<StoreDbContext>(contextOptions =>
        {
            contextOptions.UseSqlServer(connectionString);
        });

        return services;
    }
}