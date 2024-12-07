using FindIt.Domain.IdentityEntities;
using FindIt.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace FindIt.Server.ServicesExtensions
{
    public static class IdentityConfigurationsExtension
    {
        public static IServiceCollection AddIdentityConfigurations(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(identityOptions =>
            {
                identityOptions.Password.RequireLowercase = true;
                identityOptions.Password.RequireUppercase = true;
                identityOptions.Password.RequireDigit = false;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequiredUniqueChars = 3;

            }).AddEntityFrameworkStores<StoreDbContext>().AddDefaultTokenProviders();

            return services;
        }
    }
}
