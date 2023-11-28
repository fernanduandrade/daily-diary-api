using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;
using DailyDiary.API;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Domain.User;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTest.Setup;

public class AppFixture<TProgram> : IDisposable where TProgram : class
{
    public HttpClient Client;
    public ApiFactory<TProgram> Factory;

    public AppFixture()
    {
        Factory = new ApiFactory<TProgram>();
        Client = Factory.CreateClient();
        var scope = Factory.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetService<Seeder>();
        seeder.AddData();
    }
    
    public async Task<string> Login()
    {
        var payload = new { Email = "usertest@tester.com", Password = "Juazeiro0." };
        var response = await Client.PostAsJsonAsync("api/users/login", payload);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ApiResponse<UserLoggedDto>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});

        return result.Data.Token;
    }
    
    public User GenerateUser(string nameInput, string emailInput, string passwordInput)
    {
        var id = new Faker().Random.Guid();
        Email email = Email.Create(emailInput).Value;
        Password password = Password.Create(passwordInput).Value;
        var user = User.Create(email, nameInput, password);
        user.Id = id;
        return user;
    }
    public void Dispose()
    {
        Client.Dispose();
        Factory.Dispose();
    }
}

[CollectionDefinition(nameof(AppCollectionFixture))]
public class AppCollectionFixture : ICollectionFixture<AppFixture<Program>>{}