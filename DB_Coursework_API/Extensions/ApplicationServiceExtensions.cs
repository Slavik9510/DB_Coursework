using DB_Coursework_API.Data;
using DB_Coursework_API.Helpers;
using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Services;

namespace DB_Coursework_API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductsRepository, ProductsRepository>();

            return services;
        }
    }
}
