using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Api.IntegrationTest.Setup;
using DailyDiary.API;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.CreateDiary;
using DailyDiary.Application.Diaries.Dto;
using DailyDiary.Application.Diaries.UpdateDiary;
using DailyDiary.Domain.Common;
using FluentAssertions;

namespace Api.IntegrationTest.Controllers;

[Collection(nameof(AppCollectionFixture))]
public class DiariesControllerTest
{
    private readonly AppFixture<Program> _fixture;

    public DiariesControllerTest(AppFixture<Program> fixture)
    {
        _fixture = fixture;
        string token = _fixture.Login().GetAwaiter().GetResult();
        _fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            token);
    }

    [Fact(DisplayName = "Should not be able to create more than one diary per day")]
    [Trait("Api ", "Diary")]
    public async Task CreateMoreThanOneDiary_Should_Return400()
    {
        CreateDiaryCommand payload = new(
            new Guid("8636d1c9-e331-4da6-959f-d2133f754fda"), "Happy day", "content", "happy",
            true);

        var response = await _fixture.Client.PostAsJsonAsync("api/diaries", payload);
        string responseString = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var result = JsonSerializer.Deserialize<Error>(responseString,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        result.Code.Should().Be("Invalid Date");
    }
    
    [Fact(DisplayName = "Should create a diary")]
    [Trait("Api ", "Diary")]
    public async Task CreateDiary_Should_Return201()
    {
        CreateDiaryCommand payload = new(
            new Guid("5b359013-c291-4e89-9274-877dfeb85d02"),
            "Things getting better",
            "content",
            "happy",
            true);

        var response = await _fixture.Client.PostAsJsonAsync("api/diaries", payload);
        string responseString = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var result = JsonSerializer.Deserialize<ApiResponse<DiaryDto>>(responseString,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        result.Data.Title.Should().Be("Things getting better");
        result.Data.Mood.Should().Be("happy");
        result.Data.Text.Should().Be("content");
        result.Data.IsPublic.Should().Be(true);
    }
    
    [Fact(DisplayName = "Should return an empty list of user diaries")]
    [Trait("Api ", "Diary")]
    public async Task ListOfUserDiaries_Should_Return200()
    {
        Guid userId = new Guid("5b359013-c291-4e89-9274-877dfeb85d02");

        var response = await _fixture.Client.GetAsync($"api/diaries/users/{userId}");
        string responseString = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = JsonSerializer.Deserialize<ApiResponse<List<DiaryDto>>>(responseString,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        result.Data.Count.Should().Be(0);
    }
    
    [Fact(DisplayName = "Should delete a diary by id")]
    [Trait("Api ", "Diary")]
    public async Task DeleteDiary_Should_Return204()
    {
        Guid diaryId = new Guid("4510804c-4d88-4916-bfab-a37e13e32760");

        var response = await _fixture.Client.DeleteAsync($"api/diaries/{diaryId}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact(DisplayName = "Should return a diary by id")]
    [Trait("Api ", "Diary")]
    public async Task GetDiaryById_Should_Return200()
    {
        Guid diaryId = new Guid("e89e9fd4-99c3-4f91-a946-7184da2314bc");

        var response = await _fixture.Client.GetAsync($"api/diaries/{diaryId}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact(DisplayName = "Should return not found diary by id")]
    [Trait("Api ", "Diary")]
    public async Task GetDiaryById_Should_Return400()
    {
        Guid diaryId = new Guid("e89e9fd4-99c3-4f91-a946-7184da2314bd");

        var response = await _fixture.Client.GetAsync($"api/diaries/{diaryId}");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact(DisplayName = "Should update a diary id")]
    [Trait("Api ", "Diary")]
    public async Task UpdateDiary_Should_Return200()
    {
        UpdateDiaryCommand payload = new()
        {
            Id = new Guid("e89e9fd4-99c3-4f91-a946-7184da2314bc"),
            UserId = new Guid("8636d1c9-e331-4da6-959f-d2133f754fda"),
            Mood = "happy",
            Title = "Hard",
            Text = "great content"
        };
        

        var response = await _fixture.Client.PutAsJsonAsync($"api/diaries", payload);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
}