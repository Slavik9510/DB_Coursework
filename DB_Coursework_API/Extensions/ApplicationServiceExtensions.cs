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
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddSingleton<IMyLogger>(new FileLogger("C:\\Users\\Slavik\\Documents\\Visual Studio 2022\\Projects\\DB_Coursework_API\\log"));
            services.AddSingleton<IMyLogReader>(new FileLogReader("C:\\Users\\Slavik\\Documents\\Visual Studio 2022\\Projects\\DB_Coursework_API\\log"));
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<IStatisticRepository, StatisticRepository>();

            return services;
        }
    }
}
