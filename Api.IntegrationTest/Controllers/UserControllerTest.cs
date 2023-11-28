using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Api.IntegrationTest.Setup;
using DailyDiary.API;
using DailyDiary.Application.Users.CreateUser;
using DailyDiary.Domain.Common;
using FluentAssertions;

namespace Api.IntegrationTest.Controllers;

[Collection(nameof(AppCollectionFixture))]
public class UserControllerTest
{
    private readonly AppFixture<Program> _fixture;

    public UserControllerTest(AppFixture<Program> fixture)
    {
        _fixture = fixture;
        string token = _fixture.Login().GetAwaiter().GetResult();
        _fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    [Fact(DisplayName = "Should not create user when email registered")]
    [Trait("Api", "User")]
    public async Task Should_NotCreateUser_WhenEmail_IsRegistered()
    {
        
        CreateUserCommand payload = new("Joaozin", "joaozin123@tester.com", "testpassword");
        var response = await _fixture.Client.PostAsJsonAsync("api/users", payload);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        string responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Error>(responseString, new JsonSerializerOptions() {  PropertyNameCaseInsensitive = true });
        result.Code.Should().Be("Invalid email");
    }
}