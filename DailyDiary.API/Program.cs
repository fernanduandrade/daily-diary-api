using System.Text;
using DailyDiary.API.Setup;
using DailyDiary.Application;
using DailyDiary.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            builder.Services.AddAuth(configuration);
            builder.Services.AddSwagger();

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

            app.MapControllers();
            app.MapHealthChecks("/_health");
            app.Run();


            #endregion

        }
    }
}