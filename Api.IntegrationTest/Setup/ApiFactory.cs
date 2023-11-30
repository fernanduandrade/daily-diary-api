using DailyDiary.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Api.IntegrationTest.Setup;

public class ApiFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram :
    class
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithDatabase("diary")
        .WithUsername("user_postgres")
        .WithPassword("postgres")
        .WithCleanUp(true)
        .Build();

    public ApiFactory()
    {
        _dbContainer.StartAsync().GetAwaiter().GetResult();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development")
            .ConfigureTestServices(services =>
            {

                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor is not null) services.Remove(descriptor);
                
                services
                    .AddDbContext<AppDbContext>(options => { options.UseNpgsql(_dbContainer.GetConnectionString()); });

                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();
                var scopedServices = scope.ServiceProvider;

                var config = scopedServices.GetRequiredService<IConfiguration>();
                var contextClient = scopedServices.GetRequiredService<AppDbContext>();
                contextClient.Database.EnsureCreated();

                services.AddScoped<Seeder>();
            });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}