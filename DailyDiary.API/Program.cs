using DailyDiary.API.Middlewares;
using DailyDiary.API.Setup;
using DailyDiary.Application;
using DailyDiary.Infrastructure;
using Serilog;

namespace DailyDiary.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region builder

            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApplication();
            builder.Services.AddPersistence();
            builder.Services.AddInfraDependecies(configuration);
            builder.Services.AddHealthChecks();
            builder.Services.AddBehaviour();
            builder.Services.AddAuth(configuration);
            builder.Services.AddSwagger();
            builder.Configuration.ConfigureLog();
            builder.Host.UseSerilog(Log.Logger);
            
            #endregion

            #region app

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.MapControllers();
            app.MapHealthChecks("/_health");
            app.Run();


            #endregion

        }
    }
}