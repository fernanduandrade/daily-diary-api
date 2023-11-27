using DailyDiary.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DailyDiary.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfraDependecies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                config =>
                {
                    config.MigrationsAssembly("DailyDiary.API");
                }));
        
        return services;
    }
    
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services;
    }
}