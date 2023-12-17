using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Api.IntegrationTest.Setup;
using DailyDiary.API;
using DailyDiary.Application.Likes.CreateLike;
using FluentAssertions;

namespace Api.IntegrationTest.Controllers;

[Collection(nameof(AppCollectionFixture))]
public class LikesControllerTest
{
    private readonly AppFixture<Program> _fixture;

    public LikesControllerTest(AppFixture<Program> fixture)
    {
        _fixture = fixture;
        string token = _fixture.Login().GetAwaiter().GetResult();
        _fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            token);
    }

    [Fact(DisplayName = "Should be able to like a diary")]
    [Trait("Api ", "Like")]
    public async Task Like_ADiary_Should_Return200()
    {
        CreateLikeCommand payload = new(new Guid("5b359013-c291-4e89-9274-877dfeb85d02"), new Guid("e89e9fd4-99c3-4f91-a946-7184da2314bc"));
        var response = await _fixture.Client.PostAsJsonAsync("api/likes", payload);
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}