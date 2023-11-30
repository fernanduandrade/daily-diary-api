using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;

namespace DailyDiary.API.Setup;

public static class DependecyInjection
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Daily Diary", Version = "v1", Contact = new OpenApiContact()
                {
                    Name = "Developer",
                    Email = "onanduandrade@gmail.com",
                    Url = new Uri("https://github.com/fernanduandrade")
                }
            });
            config.EnableAnnotations();

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Description = "Please enter into field the word 'Bearer' following by space and JWT",
                Name = "Authorization",
                Type = SecuritySchemeType.OAuth2,
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            });
            
            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }
    
    public static void ConfigureLog(this IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }

    public static IServiceCollection AddBehaviour(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        return services;
    }
}