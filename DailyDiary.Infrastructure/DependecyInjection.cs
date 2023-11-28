using System.Text;
using DailyDiary.Domain.User;
using DailyDiary.Infrastructure.Persistence.Data;
using DailyDiary.Infrastructure.Persistence.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKeys = new[] { new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])) }

                };
            });

        return services;
    }
}