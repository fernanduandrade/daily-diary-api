using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Api.IntegrationTest.Setup;
using DailyDiary.API;
using DailyDiary.Application.Common.Models;
using DailyDiary.Application.Likes.Favorite;
using FluentAssertions;
using MediatR;

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
    public async Task Like_Diary_Should_Return200()
    {
        FavoriteCommand payload = new(
            new Guid("8636d1c9-e331-4da6-959f-d2133f754fda"),
            new Guid("e89e9fd4-99c3-4f91-a946-7184da2314bc"));
        var response = await _fixture.Client.PostAsJsonAsync("api/likes/favorite", payload);
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }
    
    [Fact(DisplayName = "Should return error message if diary has already been liked")]
    [Trait("Api ", "Like")]
    public async Task Like_Diary_Twice_Should_Return400()
    {
        FavoriteCommand payload = new(
            new Guid("8636d1c9-e331-4da6-959f-d2133f754fda"),
            new Guid("4510804c-4d88-4916-bfab-a37e13e32760"));
        var response = await _fixture.Client.PostAsJsonAsync("api/likes/favorite", payload);
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact(DisplayName = "Should return error if user tries to unlike something that has been favortied")]
    [Trait("Api ", "Like")]
    public async Task UnLike_A_Diary_ThatHasBeenLiked_Should_Return400()
    {
        FavoriteCommand payload = new(
            new Guid("8636d1c9-e331-4da6-959f-d2133f754fda"),
            new Guid("e89e9fd4-99c3-4f91-a946-7184da2314bc"));
        var response = await _fixture.Client.PostAsJsonAsync("api/likes/unfavorite", payload);
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact(DisplayName = "Should return ok when user unfavorite a driary")]
    [Trait("Api ", "Like")]
    public async Task UnLike_A_Diary_Should_Return200()
    {
        FavoriteCommand payload = new(
            new Guid("8636d1c9-e331-4da6-959f-d2133f754fda"),
            new Guid("4510804c-4d88-4916-bfab-a37e13e32760"));
        var response = await _fixture.Client.PostAsJsonAsync("api/likes/unfavorite", payload);
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }
    
}