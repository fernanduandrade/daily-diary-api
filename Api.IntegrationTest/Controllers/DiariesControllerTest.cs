
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Api.IntegrationTest.Setup;
using DailyDiary.API;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Diaries.CreateDiary;
using DailyDiary.Application.Diaries.DTO;
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
        _fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    [Fact(DisplayName = "Should not be able to create more than one diary per day")]
    [Trait("Api ", "Diary")]
    public async Task CreateMoreThanOneDiary_Should_Returns400()
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
    public async Task CreateDiary_Should_Returns200()
    {
        CreateDiaryCommand payload = new(
            new Guid("5b359013-c291-4e89-9274-877dfeb85d02"), "Things getting better", "content", "happy",
            true);

        var response = await _fixture.Client.PostAsJsonAsync("api/diaries", payload);
        string responseString = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = JsonSerializer.Deserialize<ApiResponse<DiaryDto>>(responseString,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        result.Data.Title.Should().Be("Things getting better");
        result.Data.Mood.Should().Be("happy");
        result.Data.Text.Should().Be("content");
        result.Data.IsPublic.Should().Be(true);
    }
}