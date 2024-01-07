using DailyDiary.API.Controllers.Base;
using DailyDiary.Application.Likes.CreateFavorite;
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
    public async Task<IActionResult> Favorite(CreateFavoriteCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
    
    // [SwaggerOperation(Summary = "Operation when the user unlikes a diary")]
    // [ProducesResponseType(204)]
    // [HttpPost]
    // public async Task<IActionResult> Favorite(CreateFavoriteCommand command)
    // {
    //     await Mediator.Send(command);
    //     return NoContent();
    // }
}