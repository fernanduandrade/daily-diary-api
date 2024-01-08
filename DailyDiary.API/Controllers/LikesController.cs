using DailyDiary.API.Controllers.Base;
using DailyDiary.Application.Likes.Favorite;
using DailyDiary.Application.Likes.UnFavorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DailyDiary.API.Controllers;

[Authorize]
public class LikesController : BaseController
{
    [SwaggerOperation(Summary = "Operation when the user likes a diary")]
    [ProducesResponseType(204)]
    [HttpPost("favorite")]
    public async Task<IActionResult> Favorite(FavoriteCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    [SwaggerOperation(Summary = "Operation when the user unlikes a diary")]
    [ProducesResponseType(204)]
    [HttpPost("unfavorite")]
    public async Task<IActionResult> Favorite(UnfavoriteCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}