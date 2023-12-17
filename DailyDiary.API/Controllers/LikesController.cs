using DailyDiary.API.Controllers.Base;
using DailyDiary.Application.Likes.CreateLike;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DailyDiary.API.Controllers;

[Authorize]
public class LikesController : BaseController
{
    [SwaggerOperation(Summary = "Creates a diary")]
    [ProducesResponseType(204)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateLikeCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}