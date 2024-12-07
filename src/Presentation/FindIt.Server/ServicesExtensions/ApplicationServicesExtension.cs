using FindIt.Application.Interfaces;
using FindIt.Application.Services;
using FindIt.Domain.Interfaces;
using FindIt.Persistence.Repositories.Classes;

namespace FindIt.Server.ServicesExtensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IAuthService, AuthService>();

            services.AddAutoMapper(typeof(BrandProfileMapExtension));
           
            services.AddScoped<IBrandService, BrandService>();

            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
