using DailyDiary.API.Controllers.Base;
using DailyDiary.Application.Likes.Favorite;
using DailyDiary.Application.Likes.UnFavorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DailyDiary.API.Controllers;

public class LikesController : BaseController
{
    [SwaggerOperation(Summary = "Operation when the user likes a diary")]
    [ProducesResponseType(204)]
    [HttpPost("favorite")]
    public async Task<IActionResult> Favorite(FavoriteCommand command)
    {
        var result = await Mediator.Send(command);
        if(!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
    
    [SwaggerOperation(Summary = "Operation when the user unlikes a diary")]
    [ProducesResponseType(204)]
    [HttpPost("unfavorite")]
    public async Task<IActionResult> Favorite(UnfavoriteCommand command)
    {
        var result = await Mediator.Send(command);
        if(!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}