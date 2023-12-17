using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Api.IntegrationTest.Setup;
using DailyDiary.API;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Users.CreateUser;
using DailyDiary.Application.Users.Dto;
using DailyDiary.Application.Users.LoginUserQuery;
using DailyDiary.Domain.Common;
using FluentAssertions;

namespace Api.IntegrationTest.Controllers;

[Collection(nameof(AppCollectionFixture))]
public class UsersControllerTest
{
    private readonly AppFixture<Program> _fixture;

    public UsersControllerTest(AppFixture<Program> fixture)
    {
        _fixture = fixture;
        string token = _fixture.Login().GetAwaiter().GetResult();
        _fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer", token);
    }

    [Fact(DisplayName = "Should not create user when email registered")]
    [Trait("Api", "User")]
    public async Task Should_NotCreateUser_WhenEmail_IsRegistered()
    {
        
        CreateUserCommand payload = new("Joaozin", "joaozin123@tester.com", "testpassword");
        var response = await _fixture.Client.PostAsJsonAsync("api/users", payload);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        string responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Error>(responseString,
            new JsonSerializerOptions() {  PropertyNameCaseInsensitive = true });
        result.Code.Should().Be("Invalid email");
    }
    
    [Fact(DisplayName = "Should not create user when email registered")]
    [Trait("Api", "User")]
    public async Task CreateUser_Should_201()
    {
        
        CreateUserCommand payload = new("Lari", "larri@tester.com", "testpassword123");
        var response = await _fixture.Client.PostAsJsonAsync("api/users", payload);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        string responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ApiResponse<UserDto>>(responseString, 
            new JsonSerializerOptions() {  PropertyNameCaseInsensitive = true });
        result.Data.Email.Should().Be("larri@tester.com");
        result.Data.Id.ToString().Should().NotBeNull();
    }
    
    [Fact(DisplayName = "Should error if email or password are incorrect")]
    [Trait("Api", "Login User")]
    public async Task Should_ReturnError_WhenInput_IsIncorrect()
    {
        
        LoginUserQuery payload = new("usertest@tester.com" , "wrong password");
        var response = await _fixture.Client.PostAsJsonAsync("api/users/login", payload);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        string responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Error>(responseString,
            new JsonSerializerOptions() {  PropertyNameCaseInsensitive = true });
        result.Code.Should().Be("Not Found");
    }
    
    [Fact(DisplayName = "Should return user by id if is registered")]
    [Trait("Api", "Login User")]
    public async Task Should_ReturnUserById_WhenExists()
    {
        Guid userId = new Guid("5b359013-c291-4e89-9274-877dfeb85d02");
        var response = await _fixture.Client.GetAsync($"api/users/{userId}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        string responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ApiResponse<UserDto>>(responseString,
            new JsonSerializerOptions() {  PropertyNameCaseInsensitive = true });
        result.Data.Id.Should().Be("5b359013-c291-4e89-9274-877dfeb85d02");
        result.Data.Email.Should().Be("usertest@tester.com");
    }
}